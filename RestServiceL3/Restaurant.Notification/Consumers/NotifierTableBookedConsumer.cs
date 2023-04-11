using MassTransit;
using Restaurant.Messages;
using System.Threading.Tasks;

namespace Restaurant.Notification.Consumers
{
    internal class NotifierTableBookedConsumer : IConsumer<ITableBooked>
    {
        private readonly Notifier _notifier;

        public NotifierTableBookedConsumer(Notifier notifire)
        {
            _notifier = notifire;
        }

        public Task Consume(ConsumeContext<ITableBooked> context)
        {
            var result = context.Message.Success;

            _notifier.Accept(context.Message.OrderId,
                result ? Accepted.BookingOk : Accepted.BookingError, context.Message.ClientId);

            return Task.CompletedTask;
        }
    }
}
