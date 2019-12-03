using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderWrite.Models;

namespace OrderWrite.Commands
{
    public interface ICommand
    {
    }

    public class CreateOrder: ICommand
    {
        public int OrderId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderLineItems> OrderItems { get; set; }
    }
}
