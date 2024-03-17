using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class User : IdentityUser<Guid>
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ImagePath { get; set; } = null;
        public List<UserRoles> UserRoles { get; set; } = new List<UserRoles>();

    }
}
