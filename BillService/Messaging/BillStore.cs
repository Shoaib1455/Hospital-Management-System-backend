using System.Collections.Concurrent;
using BillService.Models;

namespace BillService.Messaging
{
    public class BillStore
    {
        private readonly ConcurrentQueue<Bill> _bills = new();

        public void Add(Bill bill) => _bills.Enqueue(bill);

        public IReadOnlyList<Bill> GetAll() => _bills.ToList();
    }
}
