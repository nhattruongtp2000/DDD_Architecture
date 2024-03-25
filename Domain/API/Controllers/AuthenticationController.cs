using Application.Authentication.Commands;
using Application.Authentication.Commands.Register;
using Application.Authentication.Queries.Login;
using Application.Authentication.Queries.Token;
using Azure.Core;
using Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ListErrorsApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
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
            var query = _mapper.Map<LoginQuery>(request);
            var authResult = await _mediator.Send(query);

            //you can custom config 
            if (authResult.IsError)
            {
                // write anything to modify
            }

            return authResult.Match(authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }
            var query = _mapper.Map<TokenQuery>(tokenModel);
            var authResult = await _mediator.Send(query);

            //you can custom config 
            if (authResult.IsError)
            {
                // write anything to modify
            }

            return authResult.Match(authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }

        //[Authorize]
        //[HttpPost]
        //[Route("revoke-all")]
        //public async Task<IActionResult> RevokeAll()
        //{
        //    var users = _userManager.Users.ToList();
        //    foreach (var user in users)
        //    {
        //        user.RefreshToken = null;
        //        await _userManager.UpdateAsync(user);
        //    }

        //    return NoContent();
        //}
    }
}
