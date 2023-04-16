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
            for (ushort i = 1; i <= 2; i++)
                CurrentTeables.Add(new Table(i));
        }

        public async Task<Table> BookFreeTableAsync(int countOfPersons)
        {
            Console.WriteLine("Подожди я подберу столик. Ожидай СМС с подтверждением!");
            Thread.Sleep(4000);

            Table table = await Task.Run(() => GetTable(countOfPersons));
            if (table == null)
                throw new Exception("Все столики заняты.");

            return table;
        }

        private Table GetTable(int countOfPersons)
        {
            Table table = null;
            lock (CurrentTeables)
            {
                table = CurrentTeables.FirstOrDefault(t => t.SeatsCount > countOfPersons - 1 && t.CurrentState == BookingState.Free);
                table?.SetState(BookingState.Booked);
            }

            return table;
        }
    }
}
