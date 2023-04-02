using Rest.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestService.Services
{
    internal class BookService
    {
        private Restaurant _restaurant;
        
        public BookService(Restaurant restaurant)
        {
            _restaurant = restaurant;
        }
        
        public void BookFreeTable(int countOfPersons)
        {
            Console.WriteLine("Подожди я подберу столик, оставайся на линии");

            Table table = _restaurant.CurrentTeables.FirstOrDefault(t => t.SeatsCount > countOfPersons - 1 && t.CurrentState == State.Free);
            
            Thread.Sleep(5000);
            table?.SetState(State.Booked);

            Console.WriteLine(table is null
                ? "Все занято, пнх"
                : $"Тебе крупно повезло! Все забронил, номер столика: {table.Id}");
        }

        public void BookFreeTableAsync(int countOfPersons)
        {
            Console.WriteLine("Подожди я подберу столик, тебе придет СМС");

            Task.Run(async () =>
            {
                Table table = _restaurant.CurrentTeables.FirstOrDefault(t => t.SeatsCount > countOfPersons - 1 && t.CurrentState == State.Free);
                
                await Task.Delay(5000);
                table?.SetState(State.Booked);
                
                Console.WriteLine(table is null
                    ? "СМС: Все занято, пнх"
                    : $"СМС: Тебе крупно повезло! Все забронил, номер столика: {table.Id}");
            });
        }

        public void UnbookTable(int tableId)
        {
            Console.WriteLine("Не то, чтобы я расстроился... Жди, тебе ответят");

            Table table = _restaurant.CurrentTeables.FirstOrDefault(t => t.Id == tableId);

            Thread.Sleep(5000);
            table?.SetState(State.Free);

            Console.WriteLine(table is null
                ? "Таких столиков у нас нет"
                : $"Бронь снята!");
        }

        public void UnbookTableAsync(int tableId)
        {
            Console.WriteLine("Не то, чтобы я расстроился... Жди, тебе придет СМС");

            Task.Run(async () =>
            {
                Table table = _restaurant.CurrentTeables.FirstOrDefault(t => t.Id == tableId);

                await Task.Delay(5000);
                table?.SetState(State.Free);

                Console.WriteLine(table is null
                    ? "СМС: Таких столиков у нас нет"
                    : $"СМС: Бронь снята!");
            });
        }
    }
}
