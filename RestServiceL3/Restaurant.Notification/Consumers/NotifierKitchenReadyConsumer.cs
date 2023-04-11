using MassTransit;
using Restaurant.Messages;
using System.Threading.Tasks;

namespace Restaurant.Notification.Consumers
{
    internal class NotifierKitchenReadyConsumer : IConsumer<IKitchenReady>
    {
        private readonly Notifier _notifier;

        public NotifierKitchenReadyConsumer(Notifier notifire)
        {
            _notifier = notifire;
        }

        public Task Consume(ConsumeContext<IKitchenReady> context)
        {
            var result = context.Message.IsReady;

            _notifier.Accept(context.Message.OrderId,
                result ? Accepted.KitchenOk : Accepted.KitchenError);

            return Task.CompletedTask;
        }
    }
}
