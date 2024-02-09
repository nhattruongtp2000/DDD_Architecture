using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Roles : IdentityRole<Guid>
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }
        public List<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}
