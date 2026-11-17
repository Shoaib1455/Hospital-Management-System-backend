using System.Collections.Concurrent;
using NotificationService.Models;

namespace NotificationService.Messaging
{
    public class NotificationStore
    {
        private readonly ConcurrentQueue<Notification> _notifications = new();

        public void Add(Notification notification) => _notifications.Enqueue(notification);

        public IReadOnlyList<Notification> GetAll() => _notifications.ToList();
    }
}
