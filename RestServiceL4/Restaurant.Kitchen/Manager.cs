using MassTransit;
using Restaurant.Messages;
using Restaurant.Messages.Interfaces;
using System;
using System.Threading.Tasks;

namespace Restaurant.Kitchen
{
    public class Manager
    {
        private readonly IBus _bus;

        public Manager(IBus bus)
        {
            _bus = bus;
        }

        public async Task CheckKitchenReady(Guid orderId, Dish? dish)
        {
            var random = new Random();
            await _bus.Publish<IKitchenReady>(new KitchenReady(orderId, dish, random.Next(3) == 1));
        }
    }
}
