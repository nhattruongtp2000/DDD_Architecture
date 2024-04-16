using Application.Authentication.Queries.Login;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.ClientOrder
{
    public class ClientOrdersQueryHandler : IRequestHandler<ClientOrdersQuery, ErrorOr<DataResult>>
    {


        Task<ErrorOr<DataResult>> IRequestHandler<ClientOrdersQuery, ErrorOr<DataResult>>.Handle(ClientOrdersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
