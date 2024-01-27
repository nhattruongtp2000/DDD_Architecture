using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Contracts.Authentication;
using Domain.Common.Errors;
using Domain.Entites;
using ErrorOr;

namespace Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUserRepository    _userRepository;

        public AuthenticationService(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
        {
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return Errors.User.DuplicateEmail;
            }

            var userId = Guid.NewGuid();
            var token = _tokenGenerator.GenerateToken(new User{FirstName = firstName,LastName = lastName,Email = email,Password = password});
            return new AuthenticationResult(new User { FirstName = firstName, LastName = lastName, Email = email, Password = password },
                token
            );
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            if (user.Password != password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var token = _tokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);

        }
    }
}
