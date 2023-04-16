using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages.Interfaces
{
    public interface IBookingRequest : ITableBooked
    {
        DateTime CreationDate { get; set; }
    }
}
