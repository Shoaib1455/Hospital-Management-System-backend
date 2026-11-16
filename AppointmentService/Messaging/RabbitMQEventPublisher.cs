using AppointmentService.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace AppointmentService.Messaging
{
    public class RabbitMQEventPublisher : IEventPublisher
    {
        private static readonly string[] SubscriberQueues =
        {
            "bill.appointment.created",
            "notification.appointment.created",
            "patient.appointment.created",
            "doctor.appointment.created"
        };

        private readonly IConfiguration _configuration;
        private readonly ILogger<RabbitMQEventPublisher> _logger;

        public RabbitMQEventPublisher(IConfiguration configuration, ILogger<RabbitMQEventPublisher> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task PublishAsync<T>(string routingKey, T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:Host"] ?? "localhost",
                UserName = _configuration["RabbitMQ:Username"] ?? "guest",
                Password = _configuration["RabbitMQ:Password"] ?? "guest"
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: "hospital.exchange",
                type: ExchangeType.Direct,
                durable: true);

            foreach (var queue in SubscriberQueues)
            {
                await channel.QueueDeclareAsync(
                    queue: queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false);
                await channel.QueueBindAsync(
                    queue: queue,
                    exchange: "hospital.exchange",
                    routingKey: routingKey);
            }

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            var properties = new BasicProperties
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent
            };

            await channel.BasicPublishAsync(
                exchange: "hospital.exchange",
                routingKey: routingKey,
                mandatory: false,
                basicProperties: properties,
                body: body);

            _logger.LogInformation("Published {RoutingKey} to hospital.exchange", routingKey);
        }
    }
}
