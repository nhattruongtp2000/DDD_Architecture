using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public  class UserRoles
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public User User { get; set; }
        public Roles Role { get; set; }


    }
}
