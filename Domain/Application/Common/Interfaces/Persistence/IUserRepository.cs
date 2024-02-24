using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Authentication.Queries;
using Domain.Entites;

namespace Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<Domain.Entites.User> GetUserByEmail(string email);
        Task<List<Domain.Entites.User>> GetAllUser(string key);
        Task<(bool, string, string)> RegisterUser(Domain.Entites.User user);
        Task<(bool, string, string)> LoginUser(LoginQuery userData);
    }
}
