using MassTransit;
using Restaurant.Messages.Interfaces;
using System.Threading.Tasks;

namespace Restaurant.Notification.Consumers
{
    internal class NotificationNotifyConsumer : IConsumer<INotify>
    {
        private readonly Notifier _notifier;

        public NotificationNotifyConsumer(Notifier notifire)
        {
            _notifier = notifire;
        }

        public Task Consume(ConsumeContext<INotify> context)
        {
            _notifier.Notify(context.Message.OrderId, context.Message.ClientId, context.Message.Message);

            return Task.CompletedTask;
        }
    }
}
