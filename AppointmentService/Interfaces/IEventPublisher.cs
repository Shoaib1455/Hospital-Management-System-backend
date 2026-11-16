namespace AppointmentService.Interfaces
{
    public interface IEventPublisher
    {
       public Task PublishAsync<T>(string routingKey, T message);
    }
}
