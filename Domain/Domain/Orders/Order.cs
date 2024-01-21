using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Customers;

namespace Domain.Orders
{
    public class Order
    {
        private readonly HashSet<LineItem> _lineItems=new();

        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderStatus OrderStatus { get; set; }
        public IReadOnlyList<LineItem> LineItems => _lineItems.ToList();
        public static Order Create(Customer customer)
        {
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id
            };
            return order;
        }
        public void Add(Product product)
        {
            var lineItem = new LineItem(Guid.NewGuid(), Id, product.Id, product.Price);
            _lineItems.Add(lineItem);
        }


    }
}

public enum OrderStatus
{
    Doing,
    Done,
    Cancel
}

