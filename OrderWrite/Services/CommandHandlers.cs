using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderWrite.Commands;
using OrderWrite.Models;
using OrderWrite.Repository;
using OrderWrite.Events;
using OrderWrite.Infrastructure;


namespace OrderWrite.Services
{
    public interface ICommandHandler {
        void HandleCommand(ICommand cmd);
    }
    public class CommandHandlers : ICommandHandler
    {
        private IOrderRepository _repo;
        private IServicebusSender _sender;
        public CommandHandlers(IOrderRepository repo, IServicebusSender sender)
        {
            _repo = repo;
            _sender = sender;
        }
        public void HandleCommand(ICommand cmd)
        {
            switch (cmd) {
                case CreateOrder co:
                    CreateOrder command = (CreateOrder)cmd;
                    
                    CustomerName customer = CustomerName.CustomerNameFactory(
                        command.firstname, command.lastname, "hyderabad");

                    Orders o = new Orders(command.OrderId, customer, command.OrderDate, command.OrderItems);
                    _repo.NewOrder(o);
                    List<IEvents> events = o.GetChanges().ToList();
                    foreach (var e in events) {
                        _sender.SendMessage(e);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
