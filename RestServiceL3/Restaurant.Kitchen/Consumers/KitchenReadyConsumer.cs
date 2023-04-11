using MassTransit;
using Restaurant.Messages;
using System.Threading.Tasks;

namespace Restaurant.Kitchen.Consumers
{
    internal class KitchenReadyConsumer : IConsumer<IKitchenReady>
    {
        private readonly Manager _manager;

        public KitchenReadyConsumer(Manager manager)
        {
            _manager = manager;
        }

        public Task Consume(ConsumeContext<IKitchenReady> context)
        {
            var isReady = context.Message.IsReady;
            if (isReady)
                _manager.CheckDishInMenu(context.Message.OrderId, context.Message.PreOrder);

            return context.ConsumeCompleted;
        }
    }
}
