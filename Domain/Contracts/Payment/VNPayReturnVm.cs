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
        public string TerminalId { get; set; }
        public string VnPayTranId { get; set; }
        public string VnPayAmount { get; set; }
        public string BankCode { get; set; }
    }
}
