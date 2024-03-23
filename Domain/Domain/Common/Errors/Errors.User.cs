using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DuplicateEmail => Error.Conflict(
                code: "User.DuplicateEmail",
                description: "Email is already in use.");

            public static Error PasswordIsInvalid => Error.Validation(
    code: "User.PasswordIsInvalid",
    description: "Password is invalid");
        }
    }
}
