using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderWrite.Models;

namespace OrderWrite.Events
{

    public interface IEvents { }
    public class OrderCreated : IEvents
    {
        public int OrderId { get; set; }
        public CustomerName customer { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderLineItems> OrderItems { get; set; }

        public string EventType { get; set; }
    }
}
