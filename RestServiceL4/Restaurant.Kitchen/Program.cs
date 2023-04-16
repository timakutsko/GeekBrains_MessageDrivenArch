using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Restaurant.Kitchen.Consumers;
using System;

namespace Restaurant.Kitchen
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
                    x.AddConsumer<KitchenReadyConsumer>().Endpoint(e => e.Temporary = true);

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                });

                services.AddMassTransitHostedService(true);
                services.AddSingleton<Manager>();
            });
    }
}
