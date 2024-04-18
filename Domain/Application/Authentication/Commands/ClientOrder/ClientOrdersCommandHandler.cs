using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.ClientOrder
{
    public class ClientOrdersCommandHandler : IRequestHandler<ClientOrdersCommand, ErrorOr<DataResult>>
    {
        public Task<ErrorOr<DataResult>> Handle(ClientOrdersCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
