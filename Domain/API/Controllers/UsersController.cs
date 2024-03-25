using Application;
using Application.Authentication.Commands.User;
using Application.Authentication.Queries;
using Application.Authentication.Queries.Users;
using Azure.Core;
using Contracts.UsersContracts;
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

        [Route("update-user")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserByEmail(UserUpdateRequest userUpdate)
        {
            var command = _mapper.Map<UserCommand>(userUpdate);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsError)
            {
                // write anything to modify
            }
            var x = commandResult.Match(commandResult => Ok(_mapper.Map<DataResult>(commandResult)),
                errors => Problem(errors)); 
            return commandResult.Match(commandResult => Ok(_mapper.Map<DataResult>(commandResult)),
                errors => Problem(errors));

        }

        [Route("update-password")]
        [HttpPost]
        public async Task<IActionResult> UpdatePasswordByEmail(UpdatePasswordRequest userUpdate)
        {
            var command = _mapper.Map<UserCommand>(userUpdate);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsError)
            {
                // write anything to modify
            }
            return commandResult.Match(commandResult => Ok(_mapper.Map<DataResult>(commandResult)),
                errors => Problem(errors));
        }

        [Route("upload-user-image")]
        [HttpPost]
        public async Task<IActionResult> UploadImageUser( [FromForm]UserImageCreateRequest request)
        {
            var command = _mapper.Map<UserCommand>(request);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsError)
            {
                // write anything to modify
            }
            var x = commandResult.Match(commandResult => Ok(_mapper.Map<DataResult>(commandResult)),
           errors => Problem(errors));
            return commandResult.Match(commandResult => Ok(_mapper.Map<DataResult>(commandResult)),
           errors => Problem(errors));
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var files = Directory.GetFiles(@"wwwroot\assets\img");
            foreach (var file in files)
            {
                if (file.Contains(id.ToString()))
                {
                    return File(System.IO.File.ReadAllBytes(file), "image/jpeg");
                }
            }
            return null;
        }
    }
}
