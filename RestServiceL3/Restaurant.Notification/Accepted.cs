using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Notification
{
    [Flags]
    public enum Accepted
    {
        BookingError,
        BookingOk,
        KitchenOk,
        KitchenError,
        DishOk,
        DishError
    }
}
