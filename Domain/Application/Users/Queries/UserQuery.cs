﻿using Application.Authentication;
using Application.Users;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries
{
    public record UserQuery(string Key) : IRequest<ErrorOr<DataResult>>;
}
