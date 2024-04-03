using FluentValidation;

namespace Sm.Crm.Application.Features.Requests.Commands.UpdateRequest;
public class UpdateRequestValidator : AbstractValidator<UpdateRequestCommand>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Description)
            .MinimumLength(15);
    }
}
