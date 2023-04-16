using MassTransit;
using Restaurant.Messages;
using Restaurant.Messages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Kitchen.Consumers
{
    internal class KitchenReadyConsumer : IConsumer<IBookingRequest>
    {
        private readonly Manager _manager;

        public KitchenReadyConsumer(Manager manager)
        {
            _manager = manager;
        }

        public async Task Consume(ConsumeContext<IBookingRequest> context)
        {
            var rnd = new Random().Next(1000, 5000);
            Console.WriteLine($"[OrderId: {context.Message.OrderId}]: Проверка на кухне займет: {rnd} мс");
            await Task.Delay(rnd);
            
            await _manager.CheckKitchenReady(context.Message.OrderId, context.Message.PreOrder);
        }
    }
}
