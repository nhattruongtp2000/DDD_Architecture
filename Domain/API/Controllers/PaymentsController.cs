using Application.Authentication.Commands.Payment;
using Contracts.Payment;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PaymentsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("VnPay")]
        public async Task<IActionResult> VNPay(OrderInfo info)
        {
            var command = _mapper.Map<PaymentCommand>(info);
            var dataReturn = await _mediator.Send(command);
            if (string.IsNullOrEmpty(dataReturn.Value))
                return BadRequest();
            return Ok(dataReturn);
        }

        [HttpGet]
        [Route("VNPayReturn")]
        public async Task<IActionResult> VNPayReturn()
        {
            var command = _mapper.Map<PaymentCommand>("");
            var dataReturn = await _mediator.Send(command);
            if (string.IsNullOrEmpty(dataReturn.Value))
                return BadRequest();
            return Ok(dataReturn);
        }
    }
}
