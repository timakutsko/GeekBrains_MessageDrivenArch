using MassTransit;
using Restaurant.Messages;
using Restaurant.Messages.Interfaces;
using System;
using System.Threading.Tasks;

namespace Restaurant.Booking.Consumers
{
    public class BookingRequestConsumer : IConsumer<IBookingRequest>
    {
        private readonly Restaurant _restaurant;

        public BookingRequestConsumer(Restaurant restaurant)
        {
            _restaurant = restaurant;
        }

        public async Task Consume(ConsumeContext<IBookingRequest> context)
        {
            Console.WriteLine($"[OrderId: {context.Message.OrderId}]");

            Table table = await _restaurant.BookFreeTableAsync(1);

            await context.Publish<ITableBooked>(
                new TableBooked(context.Message.OrderId, context.Message.ClientId, table.Id, context.Message.PreOrder));
        }
    }
}
