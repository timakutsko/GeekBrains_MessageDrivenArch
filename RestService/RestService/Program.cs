using Rest.DAL;
using RestService.Services;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace RestService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Restaurant restaurant = new Restaurant();
            //BookService bookService = new BookService(restaurant);
            BookServiceV2 bookServiceV2 = new BookServiceV2(restaurant);

            TimerCallback tm = new TimerCallback(bookServiceV2.AutoResetBookAsync);
            Timer timer = new Timer(tm, null, 0, 5000);

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                Console.WriteLine(">>>Желаешь забронить столик, или снять бронь? \n1 - Забронириовать" +
                    "\n2 - Снять бронь");
                if (!int.TryParse(Console.ReadLine(), out int choice) && choice is not (1 or 2))
                {
                    Console.WriteLine("Необходимо вводить только предложенные команды");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Введи количество посетителей: ");
                        if (!int.TryParse(Console.ReadLine(), out int persons))
                        {
                            Console.WriteLine("Это какая-то коженная шутка, для которой я слишком робот?");
                            continue;
                        }
                        
                        bookServiceV2.BookFreeTableAsync(persons);
                        break;
                    
                    case 2:
                        Console.Write("Введи номер столика: ");
                        if (!int.TryParse(Console.ReadLine(), out int tableId))
                        {
                            Console.WriteLine("Это какая-то коженная шутка, для которой я слишком робот?");
                            continue;
                        }

                        bookServiceV2.UnBookTableAsync(tableId);
                        break;

                        //switch (choice)
                        //{
                        //    #region Book
                        //    case 1:
                        //        Console.Write(">>>Как забронировать?\n1 - скину СМС (асинхронно)" +
                        //            "\n2 - подожди на линии, тебя оповестят (синхронно): ");
                        //        if (!int.TryParse(Console.ReadLine(), out int bookChoice) && bookChoice is not (1 or 2))
                        //        {
                        //            Console.WriteLine("Необходимо вводить только предложенные команды");
                        //            continue;
                        //        }

                        //        Console.Write("Введи количество посетителей: ");
                        //        if (!int.TryParse(Console.ReadLine(), out int persons))
                        //        {
                        //            Console.WriteLine("Это какая-то коженная шутка, для которой я слишком робот?");
                        //            continue;
                        //        }

                        //        // Найдем время на бронирование
                        //        Stopwatch stopWatchBook = new Stopwatch();
                        //        stopWatchBook.Start();
                        //        switch (bookChoice)
                        //        {
                        //            case 1:
                        //                bookService.BookFreeTableAsync(persons);
                        //                break;
                        //            case 2:
                        //                bookService.BookFreeTable(persons);
                        //                break;
                        //        }

                        //        stopWatchBook.Stop();
                        //        TimeSpan tsb = stopWatchBook.Elapsed;
                        //        Console.WriteLine($"Время на обработку: {tsb.Seconds:00}:{tsb.Milliseconds:000}");
                        //        break;

                        //    #endregion

                        //    #region UnBook

                        //    case 2:
                        //        Console.Write(">>>Как снять бронь?\n1 - скину СМС (асинхронно)" +
                        //            "\n2 - подожди на линии, тебя оповестят (синхронно): ");
                        //        if (!int.TryParse(Console.ReadLine(), out int unbookChoice) && unbookChoice is not (1 or 2))
                        //        {
                        //            Console.WriteLine("Необходимо вводить только предложенные команды");
                        //            continue;
                        //        }

                        //        Console.Write("Введи номер столика: ");
                        //        if (!int.TryParse(Console.ReadLine(), out int tableId))
                        //        {
                        //            Console.WriteLine("Это какая-то коженная шутка, для которой я слишком робот?");
                        //            continue;
                        //        }

                        //        // Найдем время на бронирование
                        //        Stopwatch stopWatchUnBook = new Stopwatch();
                        //        stopWatchUnBook.Start();
                        //        switch (unbookChoice)
                        //        {
                        //            case 1:
                        //                bookService.UnbookTableAsync(tableId);
                        //                break;
                        //            case 2:
                        //                bookService.UnbookTable(tableId);
                        //                break;
                        //        }

                        //        stopWatchUnBook.Stop();
                        //        TimeSpan tsub = stopWatchUnBook.Elapsed;
                        //        Console.WriteLine($"Время на обработку: {tsub.Seconds:00}:{tsub.Milliseconds:000}");
                        //        break;

                        //        #endregion
                }
            }
        }
    }
}
