using Application;
using Application.Authentication.Queries;
using Application.User.Queries;
using Application.Users;
using Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ListErrorsApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public UsersController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("get-all")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm]UserRequest request)
        {
            var query = _mapper.Map<UserQuery>(request);
            var authResult = await _mediator.Send(query);

            //you can custom config 
            if (authResult.IsError)
            {
                // write anything to modify
            }

            return authResult.Match(authResult => Ok(_mapper.Map<DataResult>(authResult)),
                errors => Problem(errors));
        }
    }
}
