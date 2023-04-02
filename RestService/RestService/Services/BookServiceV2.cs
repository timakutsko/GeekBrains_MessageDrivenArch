using Rest.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestService.Services
{
    internal class BookServiceV2
    {
        private Restaurant _restaurant;

        public BookServiceV2(Restaurant restaurant)
        {
            _restaurant = restaurant;
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
                    table = _restaurant.CurrentTeables.FirstOrDefault(t => t.SeatsCount > countOfPersons - 1 && t.CurrentState == State.Free);
                    table?.SetState(State.Booked);

                    waitHandler.Set();
                }

                Console.WriteLine(table is null
                    ? "СМС: Все занято, пнх"
                    : $"СМС: Тебе крупно повезло! Все забронил, номер столика: {table.Id}");
            });
            thread.Start(countOfPersons);
        }

        public void UnBookTableAsync(int tableId)
        {
            Console.WriteLine("Не то, чтобы я расстроился... Жди, тебе ответят");

            AutoResetEvent waitHandler = new AutoResetEvent(true);

            Thread thread = new Thread((obj) =>
            {
                Thread.Sleep(1000);
                Table table = null;
                if (obj != null && obj is Int32)
                {
                    waitHandler.WaitOne();

                    int tableId = (int)obj;
                    table = _restaurant.CurrentTeables.FirstOrDefault(t => t.Id == tableId);
                    table?.SetState(State.Free);

                    waitHandler.Set();
                }

                Console.WriteLine(table is null
                    ? "СМС: Таких столиков у нас нет"
                    : $"СМС: Бронь снята!");
            });
            thread.Start(tableId);
        }

        /// <summary>
        /// Автоматическая отмена брони ВСЕХ столиков
        /// </summary>
        /// <param name="obj"></param>
        public void AutoResetBookAsync(object obj)
        {
            AutoResetEvent waitHandler = new AutoResetEvent(true);

            if (_restaurant.CurrentTeables.Where(t => t.CurrentState != State.Free).Count() > 0)
            {
                Thread thread = new Thread(() =>
                {
                    waitHandler.WaitOne();

                    foreach (Table t in _restaurant.CurrentTeables)
                        t.CurrentState = State.Free;

                    waitHandler.Set();
                });
                thread.Start();

                Console.WriteLine("Я снял все брони!");
            }
        }
    }
}
