using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Common.Errors;
using Domain.Entites;
using MediatR;
using Newtonsoft.Json;
using MapsterMapper;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var user = await _userRepository.GetUserByEmail(command.Email);
            if (user != null)
            {
                return Errors.User.DuplicateEmail;
            }


            var registerUser = await _userRepository.RegisterUser(_mapper.Map<Domain.Entites.User>(command));

            if (!registerUser.Item1)
                return Errors.User.PasswordIsInvalid;

            var token = registerUser.Item2;
            return new AuthenticationResult(new Domain.Entites.User { FirstName = command.FirstName, LastName = command.LastName, Email = command.Email, Password = command.Password },
                token
            );
        }
    }
}
