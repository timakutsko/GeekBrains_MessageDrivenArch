using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using Restaurant.Messages;
using System.Threading.Tasks;

namespace Restaurant.Booking
{
    internal class Worker : BackgroundService
    {
        private readonly IBus _bus;
        private readonly Restaurant _restaurant;

        public Worker(IBus bus, Restaurant restaurant)
        {
            _bus = bus;
            _restaurant = restaurant;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine(">>>Привет!!! Бронируем столик?");
                
                await Task.Delay(2000, stoppingToken);
                
                var result = await _restaurant.BookFreeTableAsync(1);
                await _bus.Publish(new TableBooked(NewId.NextGuid(), NewId.NextGuid(), result ?? false, new Dish { Name = "Яйцо"}), 
                    context => context.Durable = false, stoppingToken);
            }

        }
    }
}
