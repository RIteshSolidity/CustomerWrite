using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderWrite.Events;

namespace OrderWrite.Models
{

    //public class OrderId {
    //    private int orderid;
    //    public OrderId(int o)
    //    {
    //        if (o == default)
    //        {
    //            throw new Exception("order id cannot be null !!");
    //        }
    //    }
    //}

    public enum OrderState { 
        OrderCreated,
        OrderApproved,
        OrderRejected,
        OrderFulfilled
    }
// root aggregate
    public class Orders : Entity
    {

        public override void CheckValidity() {
            switch (state) {
                case OrderState.OrderCreated:
                    if (OrderId == default)
                    {
                        throw new OrdersException("order id is wrong !!");
                    }
                    if (OrderDate < DateTime.Now)
                    {
                        throw new OrdersException("order date is wrong !!");
                    }
                    break;
                default:
                    new OrdersException("invalid state!!");
                    break;
            }

        }
        public Orders(int orderid, CustomerName cu,  DateTime odate, List<OrderLineItems> oli )
        {
            apply(new OrderCreated() {
                OrderItems = oli,
                OrderId = orderid,
                customer = cu,
                OrderDate = odate,
                EventType = "OrderCreated"
        });
            state = OrderState.OrderCreated;
      
        }
        private Orders()
        {
            
        }

        public void UpdateOrderDate(DateTime odt) {
            OrderDate = odt;
            CheckValidity();
        }

        public override void when(IEvents myevent)
        {
            switch (state)
            {
                case OrderState.OrderCreated:
                    this.customer = ((OrderCreated)myevent).customer;
                    this.OrderDate = ((OrderCreated)myevent).OrderDate;
                    this.OrderId = ((OrderCreated)myevent).OrderId;
                    this.OrderItems = ((OrderCreated)myevent).OrderItems;
                     break;
                default:
                    new OrdersException("invalid state!!");
                    break;
            }
        }

        public OrderState state;
        public int OrderId { get; private set; }

        public CustomerName customer { get; private set; }
        public DateTime OrderDate { get; private set; }

        public List<OrderLineItems> OrderItems { get; private set; }
    }

    //  value type
    public class CustomerName: IEquatable<CustomerName> {
        public static CustomerName CustomerNameFactory(string first, string last, string location) {
            return new CustomerName(first, last, location);
        }

        public CustomerName(string _first, string _last, string _location)
        {
            if (_first == default) {
                throw new Exception("First name cannot be empty !!");
            }

            if (_last == default)
            {
                throw new Exception("Last name cannot be empty !!");
            }

            if (_location == default || _location != "hyderabad")
            {
                throw new Exception("location name cannot be empty and should be Hyderabad!!");
            }
            FirstName = _first;
            LastName = _last;
            Location = _location;
        }

        private CustomerName()
        {

        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string Location { get; private set; }

        public bool Equals(CustomerName other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return FirstName.Equals(other.FirstName) && LastName.Equals(other.LastName) && Location.Equals(other.Location);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;

            return Equals((CustomerName)obj);
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() + LastName.GetHashCode() + Location.GetHashCode();
        }

        public static bool operator ==(CustomerName left, CustomerName right) {
            return Equals(left, right);
        }

        public static bool operator !=(CustomerName left, CustomerName right) {
            return !Equals(left, right);
        }
    }

    //Entity
    public class OrderLineItems { 
        
        
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        
    }
}
