using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Employees.Commands.CreateEmployee;
public class CreateEmployeeCommand : IRequest<int>
{
	public Guid? UserId { get; set; }
	public string IdentityNumber { get; set; } = null!;
	public int? DepartmentId { get; set; }
	public DateTime? StartDate { get; set; }
	public int? StatusTypeId { get; set; }
	public int? TerritoryId { get; set; }
	public DateOnly? BirthDate { get; set; }
	public Guid? ReportsToUserId { get; set; }
	public string? PhotoPath { get; set; }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
	private readonly IEmployeeRepository _employeeRepository;
	private readonly IMapper _mapper;

	public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
	{
		_employeeRepository = employeeRepository;
		_mapper = mapper;
	}

	public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<Employee>(request);
		var id = await _employeeRepository.Create(entity);
		return id;
	}
}


