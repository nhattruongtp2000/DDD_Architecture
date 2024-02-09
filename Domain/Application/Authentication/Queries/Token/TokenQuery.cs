using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.Token
{
    public record TokenQuery(string AccessToken, string RefreshToken) : IRequest<ErrorOr<RefreshTokenResult>>;
}
