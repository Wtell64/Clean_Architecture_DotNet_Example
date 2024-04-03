using FluentValidation;
using Sm.Crm.Application.Features.Departments.Commands.CreateDepartment;

namespace Sm.Crm.Application.Features.Departments.Commands;

public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}