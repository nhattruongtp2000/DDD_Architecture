using Application;
using Application.Common.Interfaces.Payment;
using Contracts.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Payment
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PaymentRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> VNPAY(OrderInfo orderInfo)
        {
            var OrderId = DateTime.Now.Ticks.ToString();
            var notify = "";

            string vnp_Returnurl = _configuration["VNPay:vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = _configuration["VNPay:vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = _configuration["VNPay:vnp_TmnCode"]; //Ma website
            string vnp_HashSecret = _configuration["VNPay:vnp_HashSecret"]; //Chuoi bi mat

            var vnpayData = new VnpayData
            {
                BankCode = "NCB",
                CreateDate = DateTime.Now.ToString(),
                CurrCode = "VND",
                IpAddr = /*_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()*/"13.160.92.202",
                Locale = "vn",
                OrderInfo = "Thanh toan don hang:" + OrderId,
                OrderType = "other",
                ReturnUrl = vnp_Returnurl,
                TxnRef = OrderId,
                ExpireDate = "20231231", // Replace with the desired value
                BillMobile = "0123456789", // Replace with the desired value
                BillEmail = "example@example.com", // Replace with the desired value
                BillFirstName = "John", // Replace with the desired value
                BillLastName = "Doe", // Replace with the desired value
                BillAddress = "123 Main Street", // Replace with the desired value
                BillCity = "City", // Replace with the desired value
                BillCountry = "Country", // Replace with the desired value
                BillState = "", // Empty string as per the original code
                InvPhone = "0987654321", // Replace with the desired value
                InvEmail = "invoice@example.com", // Replace with the desired value
                InvCustomer = "Customer Name", // Replace with the desired value
                InvAddress = "456 Secondary Street", // Replace with the desired value
                InvCompany = "Company Name", // Replace with the desired value
                InvTaxcode = "TAX123", // Replace with the desired value
                InvType = "personal", // Replace with the desired value
            };


            DateTime currentTimeUtc = DateTime.UtcNow;

            // Chuyển múi giờ từ UTC thành GMT+7 (7 giờ sau)
            DateTime currentTimeGmtPlus7 = currentTimeUtc.AddDays(1);

            // Định dạng thời gian theo yêu cầu: "yyyyMMddHHmmss"
            string formattedTime = currentTimeGmtPlus7.ToString("yyyyMMddHHmmss");

            vnpayData.ExpireDate = formattedTime;


            if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            {
                notify = "Vui lòng cấu hình các tham số: vnp_TmnCode,vnp_HashSecret trong file web.config";
                return await Task.FromResult(notify);
            }
            //Get payment input
            OrderInfo order = new OrderInfo();
            //Save order to db

            order.OrderId = OrderId; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
            order.TotalAmount = 100000; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
            order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending"
            order.OrderContent = "Pay for Mr Michele Scofiede";
            order.CreatedDate = DateTime.Now;
            string locale = "vn";
            //Build URL for VNPAY
            VNPayLibrary vnpay = new VNPayLibrary();

            vnpay.AddRequestData("vnp_Version", VNPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.TotalAmount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            //if (cboBankCode != null && !string.IsNullOrEmpty(cboBankCode)) // loai thanh toan
            //{
            //    vnpay.AddRequestData("vnp_BankCode", cboBankCode);
            //}
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "101.101.203.233");

            vnpay.AddRequestData("vnp_Locale", "vn");


            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other //loai thanh toan
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày


            vnpay.AddRequestData("vnp_ExpireDate", vnpayData.ExpireDate);
            ////Billing
            vnpay.AddRequestData("vnp_Bill_Mobile", vnpayData.BillMobile);
            vnpay.AddRequestData("vnp_Bill_Email", vnpayData.BillEmail);
            {
                vnpay.AddRequestData("vnp_Bill_FirstName", vnpayData.BillFirstName);
                vnpay.AddRequestData("vnp_Bill_LastName", vnpayData.BillLastName);
            }
            // Using the vnpayData object to set values
            vnpay.AddRequestData("vnp_Bill_Address", vnpayData.BillAddress);
            vnpay.AddRequestData("vnp_Bill_City", vnpayData.BillCity);
            vnpay.AddRequestData("vnp_Bill_Country", vnpayData.BillCountry);
            // Invoice
            vnpay.AddRequestData("vnp_Inv_Phone", vnpayData.InvPhone);
            vnpay.AddRequestData("vnp_Inv_Email", vnpayData.InvEmail);
            vnpay.AddRequestData("vnp_Inv_Customer", vnpayData.InvCustomer);
            vnpay.AddRequestData("vnp_Inv_Address", vnpayData.InvAddress);
            vnpay.AddRequestData("vnp_Inv_Company", vnpayData.InvCompany);
            vnpay.AddRequestData("vnp_Inv_Taxcode", vnpayData.InvTaxcode);
            vnpay.AddRequestData("vnp_Inv_Type", vnpayData.InvType);
            //Add Params of 2.1.0 Version


            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return await Task.FromResult(paymentUrl);
        }

        public async Task<VNPayReturnVm> VNPayReturn()
        {
            string vnp_HashSecret = _configuration["VNPay:vnp_HashSecret"]; //Chuoi bi mat
            var vnpayData = _httpContextAccessor.HttpContext.Request.Query;
            VNPayLibrary vnpay = new VNPayLibrary();

            foreach (var s in vnpayData)
            {
                //get all querystring data
                if (!string.IsNullOrEmpty(s.Key) && s.Key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(s.Key, s.Value);
                }
            }

            var orderId = vnpay.GetResponseData("vnp_TxnRef");
            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionNo = vnpay.GetResponseData("vnp_TransactionNo");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            String vnp_SecureHash = _httpContextAccessor.HttpContext.Request.Query["vnp_SecureHash"].ToString();
            String TerminalID = _httpContextAccessor.HttpContext.Request.Query["vnp_TmnCode"].ToString();
            String bankCode = _httpContextAccessor.HttpContext.Request.Query["vnp_BankCode"].ToString();
            string transactionInfo= vnpay.GetResponseData("vnp_OrderInfo");

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
            VNPayReturnVm VNreturn = new VNPayReturnVm();
            if (checkSignature)
            {
                VNreturn.OrderId = orderId;
                VNreturn.OrderName = "Payment merchendise";
                VNreturn.TransactionId = vnpayTranId.ToString();
                VNreturn.TransactionInfo = transactionInfo;
                VNreturn.TotalAmount = vnp_Amount;
                VNreturn.CurrentCode = "VND";
                VNreturn.TransactionResponseCode = "VND";
                VNreturn.CurrentCode = vnp_ResponseCode;
                VNreturn.Message = "Success";
                VNreturn.TransactionNumber = vnp_TransactionNo.ToString();
                VNreturn.Bank = bankCode.ToString();
            }
            else
            {
                VNreturn = null;
            }
            return VNreturn;
        }
    }
}
