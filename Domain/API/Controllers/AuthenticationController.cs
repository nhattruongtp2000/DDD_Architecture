using Contracts.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = Application.Authentication.IAuthenticationService;

namespace API.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : ListErrorsApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var authResult =
                _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

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
            var authResult = _authenticationService.Login(request.Email, request.Password);

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
