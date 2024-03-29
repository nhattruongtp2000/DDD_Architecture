﻿using Application.Authentication.Queries;
using Application.Authentication;
using Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Users;

namespace Application.User.Queries
{
    public class UserQueryHandler : IRequestHandler<UserQuery, ErrorOr<DataResult>>
    {
        private readonly IUserRepository _userRepository;

        public UserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async  Task<ErrorOr<DataResult>> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var listUsers= await _userRepository.GetAllUser("123");
            return new DataResult(
              listUsers);
        }
    }
}
