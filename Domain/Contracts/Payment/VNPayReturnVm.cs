using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Payment
{
    public class VNPayReturnVm
    {
        public string OrderId { get; set; }
        public string OrderName { get; set; }
        public string TransactionId { get; set; }
        public string TransactionInfo { get; set; }
        public decimal TotalAmount { get; set; }
        public string CurrentCode { get; set; }
        public string TransactionResponseCode { get; set; }
        public string Message { get; set; }
        public string TransactionNumber { get; set; }
        public string Bank { get; set; }
    }
}
