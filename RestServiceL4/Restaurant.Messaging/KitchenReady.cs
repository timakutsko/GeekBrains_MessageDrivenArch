using Restaurant.Messages.Interfaces;
using System;

namespace Restaurant.Messages
{
    public class KitchenReady : IKitchenReady
    {
        public KitchenReady(Guid orderId, Dish? preOrder, bool success)
        {
            OrderId = orderId;
            PreOrder = preOrder;
            Success = success;
        }
        public Guid OrderId { get; }

        public Guid ClientId { get; }

        public Dish? PreOrder { get; }

        public bool Success { get; }

    }
}
