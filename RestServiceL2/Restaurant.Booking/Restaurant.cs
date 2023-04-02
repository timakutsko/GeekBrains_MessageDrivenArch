using Messaging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Booking
{
    public class Restaurant
    {
        public readonly List<Table> CurrentTeables = new List<Table>();
        private readonly Producer _producerSMS = new Producer("localhost", "sms.#");
        private readonly Producer _producerEmail = new Producer("localhost", "email.#");

        public Restaurant()
        {
            for (ushort i = 1; i <= 10; i++)
                CurrentTeables.Add(new Table(i));
        }

        public void BookFreeTableAsync(int countOfPersons)
        {
            Console.WriteLine("Подожди я подберу столик, тебе придет СМС");

            AutoResetEvent waitHandler = new AutoResetEvent(true);

            Thread thread = new Thread((obj) =>
            {
                Thread.Sleep(5000);
                Table table = null;
                if (obj != null && obj is Int32)
                {
                    waitHandler.WaitOne();

                    int countOfPersons = (int)obj;
                    table = CurrentTeables.FirstOrDefault(t => t.SeatsCount > countOfPersons - 1 && t.CurrentState == State.Free);
                    table?.SetState(State.Booked);

                    waitHandler.Set();
                }

                _producerSMS.Send(
                    table is null ? "СМС: Все занято, пнх" : $"СМС: Тебе крупно повезло! Все забронил, номер столика: {table.Id}",
                    ExchangeType.Topic);

                _producerEmail.Send(
                    table is null ? "E-mail: Все занято, пнх" : $"E-mail: Тебе крупно повезло! Все забронил, номер столика: {table.Id}",
                    ExchangeType.Topic);

                //_producer.Send(
                //    table is null ? "СМС: Все занято, пнх" : $"СМС: Тебе крупно повезло! Все забронил, номер столика: {table.Id}",
                //    ExchangeType.Fanout);

                //_producer.Send(
                //    table is null ? "СМС: Все занято, пнх" : $"СМС: Тебе крупно повезло! Все забронил, номер столика: {table.Id}",
                //    ExchangeType.Direct);
            });
            thread.Start(countOfPersons);
        }
    }
}
