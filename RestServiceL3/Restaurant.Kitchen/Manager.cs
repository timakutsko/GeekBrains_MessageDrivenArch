using MassTransit;
using Restaurant.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void CheckKitchenReady(Guid orderId, Dish? dish)
        {
            var random = new Random();
            _bus.Publish<IKitchenReady>(new KitchenReady(orderId, dish, random.Next(2) == random.Next(2)));
        }

        public void CheckDishInMenu(Guid orderId, Dish? dish)
        {
            var random = new Random();
            _bus.Publish<IDishReady>(new DishInMenu(orderId, dish, random.Next(2) == random.Next(2)));
        }
    }
}
