using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Payment
{
    public class VnpayData
    {
        public string BankCode { get; set; }
        public string CreateDate { get; set; }
        public string CurrCode { get; set; }
        public string IpAddr { get; set; }
        public string Locale { get; set; }
        public string OrderInfo { get; set; }
        public string OrderType { get; set; }
        public string ReturnUrl { get; set; }
        public string TxnRef { get; set; }
        public string ExpireDate { get; set; }
        public string BillMobile { get; set; }
        public string BillEmail { get; set; }
        public string BillFirstName { get; set; }
        public string BillLastName { get; set; }
        public string BillAddress { get; set; }
        public string BillCity { get; set; }
        public string BillCountry { get; set; }
        public string BillState { get; set; }
        public string InvPhone { get; set; }
        public string InvEmail { get; set; }
        public string InvCustomer { get; set; }
        public string InvAddress { get; set; }
        public string InvCompany { get; set; }
        public string InvTaxcode { get; set; }
        public string InvType { get; set; }
    }
}
