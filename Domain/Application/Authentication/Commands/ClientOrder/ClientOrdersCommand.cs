using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.ClientOrder
{
    public record ClientOrdersCommand (dynamic orderCommand) : IRequest<ErrorOr<DataResult>>;
}
