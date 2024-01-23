using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;

namespace Application.Authentication
{
    public record AuthenticationResult(
        User User,
        string Token);
}
