using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UsersContracts
{
    public class UserImageCreateRequest
    {
        public string Email { get; set; }
        public string Caption { get; set; }
        public bool? IsDefault { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
