using MediatR;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(int Id) : IRequest<bool>;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
	private readonly IEmployeeRepository _employeeRepository;

	public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
	{
		_employeeRepository = employeeRepository;
	}

	public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
	{
		bool isSuccess = await _employeeRepository.DeleteById(request.Id);
		return isSuccess;
	}
}