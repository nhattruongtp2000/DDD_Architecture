using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Products
{
    public class AddProductRequest
    {
        public string  ProductName { get; set;}
        public string Description { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public IFormFile PhotoReview { get; set; }
    }
}
