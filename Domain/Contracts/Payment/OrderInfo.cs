using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Payment
{
    public class OrderInfo
    {
        public string OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public long PaymentTranId { get; set; }
        public string BankCode { get; set; }
        public string PayStatus { get; set; }
        public string Language { get; set; }
    }
}
