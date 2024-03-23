using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UsersContracts
{
    public record UserUpdateRequest( UserUpdateData userUpdate) : IUserRequest;
    public record UserUpdateData(
       string FirstName,
    string LastName,
    string Email);
}
