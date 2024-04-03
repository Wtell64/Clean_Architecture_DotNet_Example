using FluentValidation;

namespace Sm.Crm.Application.Features.Requests.Commands.CreateRequest;
public class CreateRequestValidator : AbstractValidator<CreateRequestCommand>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.Description)
           .MinimumLength(15);
    }
}