using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQuery : IRequest<ICollection<EmployeesDto>>;

public class GetAllEmployeesQeuryHandler : IRequestHandler<GetAllEmployeesQuery, ICollection<EmployeesDto>>
{

	private readonly IEmployeeRepository _employeeRepository;
	private readonly IMapper _mapper;

	public GetAllEmployeesQeuryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
	{
		_employeeRepository = employeeRepository;
		_mapper = mapper;
	}

	public async Task<ICollection<EmployeesDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
	{
		var entities =
			_employeeRepository.GetAll(
				e => e.UserFk,
				e => e.DepartmentFk,
				e => e.TerritoryFk);

		return _mapper.Map<List<EmployeesDto>>(entities).ToList();
	}
}