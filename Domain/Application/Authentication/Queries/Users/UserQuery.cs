using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.Users
{
    public record UserQuery(string Key) : IRequest<ErrorOr<DataResult>>;
}
