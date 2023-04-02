using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Restaurant.Booking
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Restaurant restaurant = new Restaurant();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                Console.WriteLine(">>>Привет! Бронируем столик? ");
                
                await Task.Delay(5000);

                Stopwatch stopWatchUnBook = new Stopwatch();
                stopWatchUnBook.Start();
                restaurant.BookFreeTableAsync(1);
                stopWatchUnBook.Stop();
                
                TimeSpan tsub = stopWatchUnBook.Elapsed;
                Console.WriteLine($">>>Время на обработку: {tsub.Seconds:00}:{tsub.Milliseconds:000}");
            }
        }
    }
}
