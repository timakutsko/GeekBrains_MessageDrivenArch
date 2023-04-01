using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.DAL
{
    public class Restaurant
    {
        public readonly List<Table> CurrentTeables = new List<Table>();

        public Restaurant()
        {
            for (ushort i = 1; i <= 10; i++)
                CurrentTeables.Add(new Table(i));
        }
    }
}
