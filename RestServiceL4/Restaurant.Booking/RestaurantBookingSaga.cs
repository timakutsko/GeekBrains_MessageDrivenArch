using Automatonymous;
using MassTransit;
using Restaurant.Booking.Consumers;
using Restaurant.Messages;
using Restaurant.Messages.Interfaces;
using System;

namespace Restaurant.Booking
{
    public class RestaurantBookingSaga : MassTransitStateMachine<RestaurantBooking>
    {
        public RestaurantBookingSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => BookingRequested,
                x => x
                    .CorrelateById(context => context.Message.OrderId)
                    .SelectId(context => context.Message.OrderId));

            Event(() => TableBooked,
                x => x
                    .CorrelateById(context => context.Message.OrderId));

            Event(() => KitchenReady,
                x => x
                    .CorrelateById(context => context.Message.OrderId));

            Event(() => BookingRequestFault,
                x => x
                    .CorrelateById(context => context.Message.Message.OrderId)
                    .SelectId(context => context.Message.Message.OrderId));

            CompositeEvent(() => BookingApproved,
                x => x.ReadyEvantStatus, TableBooked, KitchenReady);

            Schedule(() => BookingExpired,
                x => x.ExpirationId, x =>
                {
                    x.Delay = TimeSpan.FromSeconds(5);
                    x.Received = e => e.CorrelateById(context => context.Message.OrderId);
                });

            Initially(
                When(BookingRequested)
                    .Then(context =>
                    {
                        context.Instance.CorrelationId = context.Data.OrderId;
                        context.Instance.OrderId = context.Data.OrderId;
                        context.Instance.ClientId = context.Data.ClientId;
                    })
                    .Schedule(BookingExpired,
                        context => new BookingExpire(context.Instance),
                        context => TimeSpan.FromSeconds(5))
                    .TransitionTo(AwaitingBookingApproved)
            );

            During(AwaitingBookingApproved,
                When(BookingApproved)
                .Unschedule(BookingExpired)
                .Publish(context =>
                    (INotify)new Notify(
                        context.Instance.OrderId,
                        context.Instance.ClientId,
                        ">>> Saga: Стол успешно забронирован!"))
                .Finalize(),

                When(BookingRequestFault)
                    .Publish(context =>
                        (INotify)new Notify(
                            context.Instance.OrderId,
                            context.Instance.ClientId,
                            ">>> Saga: Прошу прощения, ничего не вышло"))
                    .Finalize(),

                When(BookingExpired.Received)
                    .Publish(context =>
                        (INotify)new Notify(
                            context.Instance.OrderId,
                            context.Instance.ClientId,
                            $">>> Saga: Заказ {context.Instance.OrderId} - просрочен! Отмена."))
                    .Finalize()
                );

            SetCompletedWhenFinalized();
        }

        public State AwaitingBookingApproved { get; private set; }

        public Event<IBookingRequest> BookingRequested { get; private set; }

        public Event<ITableBooked> TableBooked { get; private set; }

        public Event<IKitchenReady> KitchenReady { get; private set; }

        public Event<Fault<IBookingRequest>> BookingRequestFault { get; private set; }

        public Schedule<RestaurantBooking, IBookingExpire> BookingExpired { get; private set; }

        public Event BookingApproved { get; private set; }
    }
}
