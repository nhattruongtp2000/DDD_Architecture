using Application;
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

            var dataMapper = _mapper.Map<DataResult>(dataReturn.Value);

            if (string.IsNullOrEmpty(dataMapper.Data))
                return BadRequest();
            return Ok(dataReturn);
        }

        [HttpGet]
        [Route("VNPayReturn")]
        public async Task<IActionResult> VNPayReturn()
        {
            try
            {
                var command = new PaymentCommand(null);
                var dataReturn = await _mediator.Send(command);
                var dataMapper = _mapper.Map<DataResult>(dataReturn.Value);

                if (dataMapper.Data==null)
                    return BadRequest();
                return Ok(dataReturn);
            }
            catch (Exception ex)
            {
                    return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddPayment")]
        public async Task<IActionResult> IPN()
        {
            try
            {
                //var command = new PaymentCommand(null);
                //var dataReturn = await _mediator.Send(command);
                //var dataMapper = _mapper.Map<DataResult>(dataReturn.Value);

                //if (dataMapper.Data == null)
                //    return BadRequest();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
