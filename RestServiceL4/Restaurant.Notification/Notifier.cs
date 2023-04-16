using System;

namespace Restaurant.Notification
{
    public class Notifier
    {
        public void Notify(Guid orderId, Guid clientId, string msg)
        {
            Console.WriteLine($"[OrderId: {orderId}]: Уважаемый клиент {clientId}! {msg}");
        }
    }
}
