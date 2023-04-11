using MassTransit;
using Restaurant.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Notification.Consumers
{
    internal class NotifierDishReadyConsumer : IConsumer<IDishReady>
    {
        private readonly Notifier _notifier;

        public NotifierDishReadyConsumer(Notifier notifier)
        {
            _notifier = notifier;
        }

        public Task Consume(ConsumeContext<IDishReady> context)
        {
            var result = context.Message.InMenu;

            _notifier.Accept(context.Message.OrderId,
                result ? Accepted.DishOk : Accepted.DishError);

            return context.ConsumeCompleted;
        }
    }
}
