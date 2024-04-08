using Application.Common.Interfaces.Payment;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Product;
using ErrorOr;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.Product
{
    public class ProductQueryHandler : IRequestHandler<ProductQuery, ErrorOr<DataResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<DataResult>> Handle(ProductQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var dataType = query.request?.GetType()?.Name;
            if (dataType == null)
            {
                var data = await _productRepository.GetAll();
                if (data == null)
                {
                    return new ErrorOr<DataResult>();
                }
                return new DataResult(
      data);
            }

             return new ErrorOr<DataResult>(); ;
        }
    }
}
