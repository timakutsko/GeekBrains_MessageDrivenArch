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

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(3000, stoppingToken);

                Console.WriteLine("~~~~~ Привет!!! Бронируем столик? ~~~~~");
                
                await _bus.Publish(new BookingRequest(NewId.NextGuid(), NewId.NextGuid(), new Dish { Name = "Яйцо"}),
                    stoppingToken);
            }

        }
    }
}
