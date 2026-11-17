using System.Text;
using System.Text.Json;
using BillService.Events;
using BillService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BillService.Messaging
{
    public class AppointmentCreatedConsumer : BackgroundService
    {
        private const string ExchangeName = "hospital.exchange";
        private const string QueueName = "bill.appointment.created";
        private const string RoutingKey = "appointment.created";

        private readonly IConfiguration _configuration;
        private readonly ILogger<AppointmentCreatedConsumer> _logger;
        private readonly BillStore _store;

        public AppointmentCreatedConsumer(
            IConfiguration configuration,
            ILogger<AppointmentCreatedConsumer> logger,
            BillStore store)
        {
            _configuration = configuration;
            _logger = logger;
            _store = store;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:Host"] ?? "localhost",
                UserName = _configuration["RabbitMQ:Username"] ?? "guest",
                Password = _configuration["RabbitMQ:Password"] ?? "guest",
                AutomaticRecoveryEnabled = true
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await using var connection = await factory.CreateConnectionAsync(stoppingToken);
                    await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

                    await channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Direct, durable: true, cancellationToken: stoppingToken);
                    await channel.QueueDeclareAsync(QueueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);
                    await channel.QueueBindAsync(QueueName, ExchangeName, RoutingKey, cancellationToken: stoppingToken);
                    await channel.BasicQosAsync(0, 1, false, stoppingToken);

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.ReceivedAsync += async (_, ea) =>
                    {
                        try
                        {
                            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                            var evt = JsonSerializer.Deserialize<AppointmentCreatedEvent>(json);
                            if (evt != null)
                            {
                                var bill = new Bill
                                {
                                    Id = Guid.NewGuid(),
                                    AppointmentId = evt.AppointmentId,
                                    Patientid = evt.Patientid,
                                    Doctorid = evt.Doctorid,
                                    Amount = evt.Amount != 0 ? evt.Amount : evt.ConsultationFee
                                };
                                _store.Add(bill);
                                _logger.LogInformation(
                                    "BillService created bill {BillId} for appointment {AppointmentId}",
                                    bill.Id, evt.AppointmentId);
                            }

                            await channel.BasicAckAsync(ea.DeliveryTag, false);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to process appointment.created message");
                            await channel.BasicNackAsync(ea.DeliveryTag, false, requeue: false);
                        }
                    };

                    await channel.BasicConsumeAsync(QueueName, autoAck: false, consumer, stoppingToken);
                    _logger.LogInformation("BillService listening on queue {Queue}", QueueName);

                    await Task.Delay(Timeout.Infinite, stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "RabbitMQ consumer error. Retrying in 5 seconds");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
    }
}
