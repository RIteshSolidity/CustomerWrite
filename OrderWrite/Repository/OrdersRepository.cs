using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderWrite.Models;

namespace OrderWrite.Repository
{

    public interface IOrderRepository {
        Orders GetOrder(int orderid);
        IEnumerable<Orders> GetAllOrders();

        void DeleterOrder(int orderid);

        void NewOrder(Orders order);

        void UpdateOrder(Orders order);
    }
    public class OrdersRepository : IOrderRepository
    {
        private OrderDBContext _context;
        public OrdersRepository(OrderDBContext context)
        {
            _context = context;
        }
        public void DeleterOrder(int orderid)
        {
            Orders o = _context.orders
                .Where(aa => aa.OrderId == orderid)
                .FirstOrDefault();
            _context.Remove(o);
            _context.SaveChanges();
        }

        public IEnumerable<Orders> GetAllOrders()
        {
            IEnumerable<Orders> e = _context.orders.ToList();
            return e;
        }

        public Orders GetOrder(int orderid)
        {
            Orders o = _context.orders.Where(aa => aa.OrderId == orderid).FirstOrDefault();
            return o;
        }

        public void NewOrder(Orders order)
        {
            _context.orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder(Orders order)
        {
            _context.Entry<Orders>(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
