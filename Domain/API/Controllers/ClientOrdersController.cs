using Application;
using Application.Authentication.Commands.ClientOrder;
using Application.Authentication.Commands.Payment;
using Contracts.Orders;
using Contracts.Products;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientOrdersController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public ClientOrdersController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [Route("orders-history")]
        [HttpGet]
        public IActionResult OrderHistory(Guid userId)
        {
            return Ok();
        }

        [Route("add-order")]
        [HttpGet]
        public async Task<IActionResult> AddNewOrder(ClientOrdersAddModel order)
        {
            var command=_mapper.Map<ClientOrdersCommand>(order);
            var commandResult =await _mediator.Send(command);
            if(commandResult.IsError)
            {

            }
            var isAddSuccess = _mapper.Map<DataResult>(commandResult.Value);

            if (isAddSuccess.Data == false)
                return BadRequest();
            return Ok(isAddSuccess);
        }

    }
}
