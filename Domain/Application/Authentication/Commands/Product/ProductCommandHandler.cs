using Application.Common.Interfaces.Payment;
using Application.Common.Interfaces.Product;
using Contracts.Payment;
using Contracts.Products;
using ErrorOr;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Payment
{
    public class ProductCommandHandler : IRequestHandler<ProductCommand, ErrorOr<DataResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<DataResult>> Handle(ProductCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var dataType = request.productRequest.GetType().Name;
            switch (dataType.ToString())
            {
                case "AddProductRequest":
                    var data = await _productRepository.AddNewProduct(request.productRequest);
                    if (data == null || !data)
                    {
                        return new ErrorOr<DataResult>();
                    }
                    return new DataResult(data);
                case "String":
                    var isDeleteSuccess = await _productRepository.DeleteProduct(request.productRequest);
                    if (isDeleteSuccess == null || !isDeleteSuccess)
                    {
                        return new ErrorOr<DataResult>();
                    }
                    return new DataResult(isDeleteSuccess);
                    break;
                default:
                    break;
            }

            return new ErrorOr<DataResult>();
        }
    }
}
