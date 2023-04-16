using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Restaurant.Booking.Consumers;
using System;

namespace Restaurant.Booking
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddMassTransit(x =>
                {
                    x.AddConsumer<BookingRequestConsumer>().Endpoint(e => e.Temporary = true);

                    x.AddConsumer<BookingRequestFaultConsumer>().Endpoint(e => e.Temporary = true);

                    x.AddSagaStateMachine<RestaurantBookingSaga, RestaurantBooking>()
                        .InMemoryRepository();
                    
                    x.AddDelayedMessageScheduler();
                    
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.UseDelayedMessageScheduler();
                        cfg.UseInMemoryOutbox();
                        cfg.ConfigureEndpoints(context);
                    });
                });
                
                services.AddMassTransitHostedService(true);

                services.AddTransient<RestaurantBooking>();
                services.AddTransient<RestaurantBookingSaga>();
                services.AddSingleton<Restaurant>();
                
                services.AddHostedService<Worker>();
            });
    }
}
