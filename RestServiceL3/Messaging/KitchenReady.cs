using System;

namespace Restaurant.Messages
{
    public interface IKitchenReady
    {
        public Guid OrderId { get; }

        public Dish? PreOrder { get; }

        public bool IsReady { get; }
    }

    public class KitchenReady : IKitchenReady
    {
        public KitchenReady(Guid orderId, Dish? preOrder, bool isReady)
        {
            OrderId = orderId;
            PreOrder = preOrder;
            IsReady = isReady;
        }
        public Guid OrderId { get; }

        public Dish? PreOrder { get; }

        public bool IsReady { get; }
    }
}
