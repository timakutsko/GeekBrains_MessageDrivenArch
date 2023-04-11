using System;

namespace Restaurant.Messages
{
    public interface ITableBooked
    {
        public Guid OrderId { get; }

        public Guid ClientId { get; }

        public Dish? PreOrder { get; }

        public bool Success { get; }
    }

    public class TableBooked : ITableBooked
    {
        public TableBooked(Guid orderId, Guid clientId, bool success, Dish? preOder = null)
        {
            OrderId = orderId;
            ClientId = clientId;
            Success = success;
            PreOrder = preOder;
        }

        public Guid OrderId { get; }

        public Guid ClientId { get; }

        public Dish? PreOrder { get; }

        public bool Success { get; }
    }
}
