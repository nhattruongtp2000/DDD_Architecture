using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Orders
{
    public class ClientOrdersModel
    {
        public string OrderId { get; set; }
        public Guid UserId { get; set; }
        public string AddressShip { get; set; }
        public string NumberShip { get; set; }
        public string NoticeShip { get; set; }
        public string VoucherCode { get; set; }
        public DateTime OrderDay { get; set; }
        public decimal TotalPice { get; set; }
        public string Status { get; set; }
        public string PaymentType { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
