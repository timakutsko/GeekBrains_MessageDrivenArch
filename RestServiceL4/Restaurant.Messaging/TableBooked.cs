using Restaurant.Messages.Interfaces;
using System;

namespace Restaurant.Messages
{
    public class TableBooked : ITableBooked
    {
        public TableBooked(Guid orderId, Guid clientId, int tableId, Dish? preOder = null)
        {
            OrderId = orderId;
            ClientId = clientId;
            TableId = tableId;
            Success = true;
            PreOrder = preOder;
        }

        public Guid OrderId { get; }

        public Guid ClientId { get; }

        public Dish? PreOrder { get; }

        public bool Success { get; } = false;

        public int TableId { get; }
    }
}
