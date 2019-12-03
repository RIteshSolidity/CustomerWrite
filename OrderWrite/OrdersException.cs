using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderWrite
{
    public class OrdersException : Exception
    {
        public OrdersException(string msg) : base(msg)
        {

        }
    }
}
