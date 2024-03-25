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
    public class PaymentCommandHandler : IRequestHandler<PaymentCommand, ErrorOr<string>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public PaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<string>> Handle(PaymentCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            switch (request.GetType().ToString())
            {
                case "OrderInfo":
                    var data = await _paymentRepository.VNPAY(_mapper.Map<OrderInfo>(request));
                    if (data == null)
                    {
                        return new ErrorOr<string>();
                    }
                    return data;

                    break;
                case "string":
                    var data2 = await _paymentRepository.VNPayReturn();
                    if (data2 == null)
                    {
                        return new ErrorOr<string>();
                    }
                    break;

            }

            return new ErrorOr<string>();
        }
    }
}
