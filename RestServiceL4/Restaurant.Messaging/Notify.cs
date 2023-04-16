using Restaurant.Messages.Interfaces;
using System;

namespace Restaurant.Messages
{
    public class Notify : INotify
    {
        public Notify(Guid orderId, Guid clientId, string msg)
        {
            OrderId = orderId;
            ClientId = clientId;
            Message = msg;
        }

        public Guid OrderId { get; private set; }

        public Guid ClientId { get; private set; }

        public string Message { get; private set; }
    }
}
