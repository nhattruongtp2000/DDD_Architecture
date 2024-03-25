using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.UsersContracts;
using Domain.Entites;

namespace Application.Authentication
{
    public record AuthenticationResult(
        UserModel User,
        string Token);
}
