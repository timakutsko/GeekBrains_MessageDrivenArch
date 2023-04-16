using Automatonymous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Booking
{
    public class RestaurantBooking : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public int CurrentState { get; set; }

        public Guid OrderId { get; set; }

        public Guid ClientId { get; set; }

        public int ReadyEvantStatus { get; set; }

        public Guid? ExpirationId { get; set; }
    }
}
