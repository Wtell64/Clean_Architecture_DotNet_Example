using FluentValidation;

namespace Sm.Crm.Application.Features.Sales.Commands.CreateSale;

public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
    }
}