using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Authentication.Commands.User;
using Application.Authentication.Queries;
using Contracts.UsersContracts;
using Domain.Entites;

namespace Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<Domain.Entites.User> UpdateByEmail(UserCommand updateUser);
        Task<Domain.Entites.User> UpdatePassword(UserCommand updateUser);
        Task<Domain.Entites.User> GetUserByEmail(string email);
        Task<Domain.Entites.User> GetUserById(Guid userId );
        Task<bool> UploadImage(UserCommand request);

        Task<List<Domain.Entites.User>> GetAllUser(string key);
        Task<(bool, string, string)> RegisterUser(Domain.Entites.User user);
        Task<(bool, string, string)> LoginUser(LoginQuery userData);


    }
}
