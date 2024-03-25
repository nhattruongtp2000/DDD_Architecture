using Contracts.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Payment
{
    public interface IPaymentRepository
    {
        Task<string> VNPAY(OrderInfo orderInfo);
        Task<VNPayReturnVm> VNPayReturn();
    }
}
