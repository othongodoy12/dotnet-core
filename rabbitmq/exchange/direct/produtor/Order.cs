using System;

namespace produtor
{
    public class Order
    {
        public Order(long id, long amount)
        {
            Id = id;
            Amount = amount;
            CreatedAt = LastUpdated = DateTime.UtcNow;
        }

        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdated { get; set; }
        public long Amount { get; set; }

        public void UpdateOrder(long amount)
        {
            Amount = amount;
            LastUpdated = DateTime.UtcNow;
        }
    }
}