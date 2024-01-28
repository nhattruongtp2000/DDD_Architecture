using Application.Authentication.Commands;
using Application.Authentication.Commands.Register;
using Application.Authentication.Queries;
using Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace API.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : ListErrorsApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var authResult = await
                _mediator.Send(command);

            return authResult.Match(authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)), 
                errors =>
                Problem(errors));
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query =_mapper.Map<LoginQuery>(request);
            var authResult =  await _mediator.Send(query);

            //you can custom config 
            if (authResult.IsError )
            {
                // write anything to modify
            }

            return authResult.Match(authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));


        }
    }
}
