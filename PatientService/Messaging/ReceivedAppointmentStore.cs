using System.Collections.Concurrent;
using PatientService.Events;

namespace PatientService.Messaging
{
    public class ReceivedAppointmentStore
    {
        private readonly ConcurrentQueue<AppointmentCreatedEvent> _events = new();

        public void Add(AppointmentCreatedEvent evt) => _events.Enqueue(evt);

        public IReadOnlyList<AppointmentCreatedEvent> GetAll() => _events.ToList();
    }
}
