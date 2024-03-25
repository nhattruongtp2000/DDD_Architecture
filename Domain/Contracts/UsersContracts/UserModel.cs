using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UsersContracts
{
    public class UserModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime BirthDay { get; set; }
        public string Organization { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string ZipCode { get; set; }
    }
}
