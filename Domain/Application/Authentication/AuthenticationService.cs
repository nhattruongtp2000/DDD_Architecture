using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Contracts.Authentication;
using Domain.Entites;

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

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            var userId = Guid.NewGuid();
            var token = _tokenGenerator.GenerateToken(new User{FirstName = firstName,LastName = lastName,Email = email,Password = password});
            return new AuthenticationResult(new User { FirstName = firstName, LastName = lastName, Email = email, Password = password },
                token
            );
        }

        public AuthenticationResult Login(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User with given email does not exist.");
            }

            if (user.Password != password)
            {
                throw new Exception("Invalid password.");
            }

            var token = _tokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);

        }
    }
}
