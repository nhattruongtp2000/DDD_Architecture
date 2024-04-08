using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Payment
{
    public record ProductCommand(dynamic productRequest) : IRequest<ErrorOr<DataResult>>;
}
