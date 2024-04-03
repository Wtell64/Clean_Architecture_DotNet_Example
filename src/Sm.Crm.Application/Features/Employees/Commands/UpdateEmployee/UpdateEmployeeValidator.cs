using FluentValidation;

namespace Sm.Crm.Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
{
	public UpdateEmployeeValidator()
	{
		RuleFor(x => x.Id)
			.GreaterThan(0);
	}
}