using Contracts.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Product
{
    public interface IProductRepository
    {
        Task<bool> AddNewProduct(AddProductRequest request);
        Task<bool> DeleteProduct(string ProductId);
        Task<List<ProductModel>> GetAll() ;
    }
}
