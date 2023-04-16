using Restaurant.Messages.Interfaces;
using System;

namespace Restaurant.Messages
{
    public class BookingRequest : IBookingRequest
    {
        public BookingRequest(Guid orderId, Guid clientId, Dish? preOder = null)
        {
            OrderId = orderId;
            ClientId = clientId;
            PreOrder = preOder;
            CreationDate = DateTime.Now;
        }

        public Guid OrderId { get; }

        public Guid ClientId { get; }

        public Dish? PreOrder { get; }

        public int TableId { get; }

        public bool Success { get; } = false;

        public DateTime CreationDate { get; set; }
    }
}
