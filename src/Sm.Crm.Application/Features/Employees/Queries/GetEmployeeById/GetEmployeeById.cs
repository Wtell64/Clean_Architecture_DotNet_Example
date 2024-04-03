using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeesDto?>;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeesDto>
{
	private readonly IEmployeeRepository _employeeRepository;
	private readonly IMapper _mapper;

	public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
	{
		_employeeRepository = employeeRepository;
		_mapper = mapper;
	}

	public async Task<EmployeesDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
	{
		var entity =
			await _employeeRepository.GetAll(
				e => e.UserFk,
				e => e.DepartmentFk,
				e => e.TerritoryFk).FirstOrDefaultAsync(e => e.Id.Equals(request.Id));

		return _mapper.Map<EmployeesDto>(entity);
	}
}