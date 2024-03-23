using Contracts.UsersContracts;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.User
{
    public record UserCommand(
        dynamic userUpdate) : IRequest<ErrorOr<DataResult>>;
}
