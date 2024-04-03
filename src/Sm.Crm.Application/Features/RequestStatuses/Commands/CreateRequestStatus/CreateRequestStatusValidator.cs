using FluentValidation;
using Sm.Crm.Application.Features.Customers.Commands.CreateCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Features.RequestStatuses.Commands.CreateRequestStatus;
public class CreateRequestStatusValidator : AbstractValidator<CreateRequestStatusCommand>
{
    public CreateRequestStatusValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
       
    }
}
