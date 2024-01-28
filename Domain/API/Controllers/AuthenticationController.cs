using Application.Authentication.Commands;
using Application.Authentication.Commands.Register;
using Application.Authentication.Queries;
using Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : ListErrorsApiController
    {
        private readonly ISender _mediator;

        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
            var authResult = await
                _mediator.Send(command);

            return authResult.Match(authResult => Ok(MapAuthResult(authResult)), 
                errors =>
                Problem(errors));
        }

        private static AuthenticationResponse MapAuthResult(Application.Authentication.AuthenticationResult authResult)
        {
            return new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName,
                  authResult.User.Email, authResult.Token);
        }


        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);
            var authResult =  await _mediator.Send(query);

            //you can custom config 
            if (authResult.IsError )
            {
                // write anything to modify
            }

            return authResult.Match(authResult => Ok(new AuthenticationResponse(authResult.User.Id,
                    authResult.User.FirstName, authResult.User.LastName,
                    authResult.User.Email, authResult.Token)),
                errors => Problem(errors));


        }
    }
}
