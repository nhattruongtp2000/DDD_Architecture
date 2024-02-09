using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entites;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenGenerator.Managers.Interfaces;

namespace Application.Authentication.Queries.Token
{
    public class TokenQueryHandler : IRequestHandler<TokenQuery, ErrorOr<RefreshTokenResult>>
    {
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;

        public TokenQueryHandler(IJwtTokenGenerator tokenGenerator, UserManager<User> userManager)
        {
            _tokenGenerator = tokenGenerator;
            _userManager= userManager;
        }

        public async Task<ErrorOr<RefreshTokenResult>> Handle(TokenQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var principal = _tokenGenerator.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
            {
                return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string username = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
            }

            var newAccessToken = _tokenGenerator.GenerateToken(principal.Claims.ToList());
            var newRefreshToken = _tokenGenerator.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new RefreshTokenResult(
                newAccessToken,
                 newRefreshToken
            );
        }
    }
}
