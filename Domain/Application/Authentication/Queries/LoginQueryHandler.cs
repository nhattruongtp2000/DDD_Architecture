using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entites;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (await _userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
            }

            if (user == null)
            {
                return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
            }
            var userCheckLogin = await _userRepository.LoginUser(query);
            if (userCheckLogin.Item1 == true && !string.IsNullOrEmpty(userCheckLogin.Item2))
            {
                return new AuthenticationResult(
               user,
               userCheckLogin.Item2);
            }
            return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
        }
    }
}
