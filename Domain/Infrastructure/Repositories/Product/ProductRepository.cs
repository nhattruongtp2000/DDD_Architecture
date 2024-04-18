using Application.Authentication.Commands.User;
using Application.Common.Interfaces.Product;
using Contracts.Products;
using Contracts.UsersContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly string _productContentFolder;
        private const string PRODUCT_CONTENT_FOLDER_NAME = "assets\\img\\products";
        public ProductRepository(IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _mapper = mapper;
            _productContentFolder = Path.Combine(webHostEnvironment.WebRootPath, PRODUCT_CONTENT_FOLDER_NAME);

        }
        public async Task<bool> AddNewProduct(AddProductRequest request)
        {
            using (var _context = new ApplicationDbContext())
            {
                if (request == null)
                    return false;
                var product = _mapper.Map<Domain.Entites.Product>(request);

                if (request.PhotoReview == null)
                {
                    var imagePath = await SaveFile(request.PhotoReview, product.ProductId.ToString());
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        product.PhotoReview = imagePath;
                    }
                }

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

        private async Task<string> SaveFile(IFormFile file, string productId)
        {
            try
            {
                var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";

                var filePath = Path.Combine(_productContentFolder, fileName);
                using var output = new FileStream(filePath, FileMode.Create);
                await file.OpenReadStream().CopyToAsync(output);
                return fileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<bool> DeleteProduct(string ProductId)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    var product = _context.Products.FirstOrDefault(x => x.ProductId == int.Parse(ProductId));
                    if (product != null)
                    {
                        _context.Products.Remove(product);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
