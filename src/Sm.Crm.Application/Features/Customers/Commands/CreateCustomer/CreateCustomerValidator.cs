using FluentValidation;

namespace Sm.Crm.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(250);
        RuleFor(x => x.TitleId).NotNull().WithMessage("Please specify a Title");
        RuleFor(x => x.IdentityNumber).MaximumLength(11);
    }
}