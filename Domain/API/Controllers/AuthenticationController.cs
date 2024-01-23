using Application.Authentication;
using Contracts.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
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
            var authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

            var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName,
                authResult.User.Email, authResult.Token);
            return Ok(response);
        }


        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login(request.Email, request.Password);

            var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName,
                authResult.User.Email, authResult.Token);
            return Ok(response);
        }
    }
}
