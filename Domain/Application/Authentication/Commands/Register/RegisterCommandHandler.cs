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


namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {

        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
        {
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUserByEmail(command.Email);
            if (user == null)
            {
                return Errors.User.DuplicateEmail;
            }
            

            var userId = Guid.NewGuid();
            var token = _tokenGenerator.GenerateToken(new User { FirstName = command.FirstName, LastName = command.LastName, Email = command.Email, Password = command.Password });
            return new AuthenticationResult(new User { FirstName = command.FirstName, LastName = command.LastName, Email = command.Email, Password = command.Password },
                token
            );
        }
    }
}
