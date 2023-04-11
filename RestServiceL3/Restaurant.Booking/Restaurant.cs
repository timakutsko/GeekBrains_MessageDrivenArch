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

        public Restaurant()
        {
            for (ushort i = 1; i <= 10; i++)
                CurrentTeables.Add(new Table(i));
        }

        public async Task<bool?> BookFreeTableAsync(int countOfPersons)
        {
            Console.WriteLine("Подожди я подберу столик, тебе придет СМС");

            Table table = await Task.Run(() => GetTable(countOfPersons));
            if (table == null)
                return false;
            else
                return true;
        }

        private Table GetTable(int countOfPersons)
        {
            Thread.Sleep(1000);
            
            Table table = null;
            lock (CurrentTeables)
            {
                table = CurrentTeables.FirstOrDefault(t => t.SeatsCount > countOfPersons - 1 && t.CurrentState == State.Free);
                table?.SetState(State.Booked);

                Console.WriteLine(
                    table is null ? "СМС: Все занято, пнх" : $"СМС: Тебе крупно повезло! Все забронил, номер столика: {table.Id}");
            }

            return table;
        }
    }
}
