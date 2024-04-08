using Application.Common.Interfaces.Product;
using Contracts.Products;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<bool> AddNewProduct(AddProductRequest request)
        {
            using (var _context = new ApplicationDbContext())
            {
                if (request == null)
                    return false;
                var product = _mapper.Map<Domain.Entites.Product>(request);
                if (product != null)
                {
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<List<ProductModel>> GetAll()
        {
            using (var _context = new ApplicationDbContext())
            {
                var products = await _context.Products.ToListAsync();
                return _mapper.Map<List<ProductModel>>(products);
            }
        }
    }
}
