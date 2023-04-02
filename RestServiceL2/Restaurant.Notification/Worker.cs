using Messaging;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Notification
{
    public class Worker : BackgroundService
    {
        private Consumer _consumer;

        public Worker()
        {
            Console.WriteLine("Выбери тип сообщений:\n1 - sms;\n2 - email;");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice is (1 or 2))
            {
                switch (choice)
                {
                    case 1:
                        _consumer = new Consumer("localhost", "sms.#");
                        break;
                    case 2:
                        _consumer = new Consumer("localhost", "email.#");
                        break;
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Receive((sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] Recevied {message}");
            });
        }

        #region "publish/subscribe"
        //private readonly Consumer _consumer;

        //public Worker()
        //{
        //    _consumer = new Consumer("localhost");
        //}
        
        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    _consumer.Receive((sender, args) =>
        //    {
        //        var body = args.Body.ToArray();
        //        var message = Encoding.UTF8.GetString(body);
        //        Console.WriteLine($"[x] Recevied {message}");
        //    });
        //}
        #endregion

        #region "Work Queue"
        //private readonly ConsumerQueue _consumer;

        //public Worker()
        //{
        //    _consumer = new ConsumerQueue("BookingNotification", "localhost");
        //}

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    _consumer.Receive((sender, args) =>
        //    {
        //        var body = args.Body.ToArray();
        //        var message = Encoding.UTF8.GetString(body);
        //        Console.WriteLine($"[x] Recevied {message}");
        //    });
        //}
        #endregion
    }
}
