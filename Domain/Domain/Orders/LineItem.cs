
namespace Domain.Orders
{
    public class LineItem
    {
        internal LineItem()
        {

        }
        internal LineItem(Guid id, Guid orderId, Guid productId)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
        }

        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
    }
}