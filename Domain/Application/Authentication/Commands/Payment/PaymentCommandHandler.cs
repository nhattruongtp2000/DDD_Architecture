using Application.Common.Interfaces.Payment;
using Contracts.Payment;
using ErrorOr;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Payment
{
    public class PaymentCommandHandler : IRequestHandler<PaymentCommand, ErrorOr<DataResult>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public PaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<DataResult>> Handle(PaymentCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var dataType = request.orderInfo?.GetType().Name;
            if (dataType == null)
            {
                var data3 = await _paymentRepository.VNPayReturn();
                if (data3 == null)
                {
                    return new ErrorOr<DataResult>();
                }
                       return new DataResult(
             data3);
            }
            if (dataType==null)
            {
                var data2 = await _paymentRepository.VNPayReturn();
                if (data2 == null)
                {
                    return new ErrorOr<DataResult>();
                }
                return new ErrorOr<DataResult>();
            }    
            switch (dataType.ToString())
            {
                case "OrderInfo":
                    var data = await _paymentRepository.VNPAY(_mapper.Map<OrderInfo>(request));
                    if (data == null)
                    {
                        return new ErrorOr<DataResult>();
                    }
                    return new DataResult(data);
                    break;
                case "string":
                    var data2 = await _paymentRepository.VNPayReturn();
                    if (data2 == null)
                    {
                        return new ErrorOr<DataResult>();
                    }
                    break;

            }

            return new ErrorOr<DataResult>();
        }
    }
}
