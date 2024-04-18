using Application;
using Application.Authentication.Commands.Payment;
using Application.Authentication.Commands.User;
using Application.Authentication.Queries.Product;
using Contracts.Products;
using Contracts.UsersContracts;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public ProductsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("add-product")]
        [HttpPost]
        public async Task<IActionResult> AddNewProduct([FromForm]AddProductRequest productRequest)
        {
            var command = _mapper.Map<ProductCommand>(productRequest);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsError)
            {
                // write anything to modify
            }
            var dataMapper = _mapper.Map<DataResult>(commandResult.Value);

            if (dataMapper.Data==false)
                return BadRequest();
            return Ok(dataMapper); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new ProductQuery(null);
            var commandResult = await _mediator.Send(query);
            if (commandResult.IsError)
            {
                // write anything to modify
            }
            var dataMapper = _mapper.Map<DataResult>(commandResult.Value);

            if (dataMapper.Data == null)
                return BadRequest();
            return Ok(dataMapper);
        }

        [HttpGet("image/{ProductId}")]
        public ActionResult Get(string ProductId)
        {
            var files = Directory.GetFiles(@"wwwroot\assets\img\products");
            foreach (var file in files)
            {
                if (file.Contains(ProductId.ToString()))
                {
                    return File(System.IO.File.ReadAllBytes(file), "image/jpeg");
                }
            }
            return null;
        }

        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> DeleteProduct(string ProductId)
        {
            var query = new ProductCommand(ProductId);
            var commandResult = await _mediator.Send(query);
            if (commandResult.IsError)
            {
                // write anything to modify
            }
            var dataMapper = _mapper.Map<DataResult>(commandResult.Value);

            if (dataMapper.Data == null)
                return BadRequest();
            return Ok(dataMapper);
        } 
    }
}
