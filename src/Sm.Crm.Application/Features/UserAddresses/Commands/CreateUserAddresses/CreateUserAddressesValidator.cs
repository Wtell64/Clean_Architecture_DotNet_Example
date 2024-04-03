using FluentValidation;

namespace Sm.Crm.Application.Features.UserAddresses.Commands.CreateUserAddresses;
public class CreateUserAddressesValidator: AbstractValidator<CreateUserAddressesCommand>
{
    public CreateUserAddressesValidator()
    {
        RuleFor(x => x.Address).NotEmpty().MinimumLength(3).MaximumLength(200).WithMessage("Address name must consist of at least 3 and maximum 200 characters.");
        RuleFor(x => x.City).NotEmpty().MinimumLength(3).MaximumLength(50).WithMessage("City name must consist of at least 3 and maximum 50 characters."); 
        RuleFor(x => x.Country).NotNull().MinimumLength(3).MaximumLength(100).WithMessage("Country name must consist of at least 3 and maximum 100 characters.");
        RuleFor(x => x.AddressType).NotEmpty().WithMessage("AddressType is not null.");

        
    }
}
