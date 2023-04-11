using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Notification
{
    public class Notifier
    {
        private readonly ConcurrentDictionary<Guid, Tuple<Guid?, Accepted>> _state = new ();

        public void Accept(Guid orderId, Accepted accepted, Guid? clientId = null)
        {
            _state.AddOrUpdate(orderId, new Tuple<Guid?, Accepted>(clientId, accepted),
                (guid, oldValue) => new Tuple<Guid?, Accepted>(oldValue.Item1 ?? clientId, oldValue.Item2 | accepted));

            Notify(orderId);
        }

        private void Notify(Guid orderId)
        {
            var booking = _state[orderId];

            switch (booking.Item2)
            {
                case Accepted.BookingOk:
                    Console.WriteLine($"Успешно забронировано для клиента {booking.Item1}");
                    _state.Remove(orderId, out _);
                    break;
                case Accepted.BookingError:
                    Console.WriteLine($"Гость {booking.Item1}, все столики заняты");
                    _state.Remove(orderId, out _);
                    break;
                case Accepted.KitchenOk:
                    Console.WriteLine($"Кухня готова принимать заказ {orderId}");
                    _state.Remove(orderId, out _);
                    break;
                case Accepted.KitchenError:
                    Console.WriteLine($"Кухня не работает :( Бронь {booking.Item1} - отменена");
                    _state.Remove(orderId, out _);
                    break;
                case Accepted.DishOk:
                    Console.WriteLine($"Скоро будет готово");
                    _state.Remove(orderId, out _);
                    break;
                case Accepted.DishError:
                    Console.WriteLine($"Блюдо отсутсвует :(");
                    _state.Remove(orderId, out _);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
