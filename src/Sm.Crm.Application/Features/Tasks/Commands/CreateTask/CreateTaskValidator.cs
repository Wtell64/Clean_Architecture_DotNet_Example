using FluentValidation;

namespace Sm.Crm.Application.Features.Tasks.Commands.CreateTask;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
    }
}
