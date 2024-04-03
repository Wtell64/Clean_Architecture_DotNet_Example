using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;

namespace Sm.Crm.Application.Features.Employees.Queries.GetAllEmployees;

public class GetPaginatedEmployeesQuery : IRequest<PaginatedResult<EmployeesDto>>
{
	public string? Search { get; set; } = string.Empty;
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
}

public class GetPaginatedEmployeesQueryHandler : IRequestHandler<GetPaginatedEmployeesQuery, PaginatedResult<EmployeesDto>>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public GetPaginatedEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<PaginatedResult<EmployeesDto>> Handle(GetPaginatedEmployeesQuery request, CancellationToken cancellationToken)
	{
		var entities = _context.Employees
			.Include(e => e.UserFk)
			.Include(e => e.DepartmentFk)
			.Include(e => e.StatusTypeFk)
			.Include(e => e.TerritoryFk)
			.OrderByDescending(e => e.Id)
			.ProjectTo<EmployeesDto>(_mapper.ConfigurationProvider);

		return await PaginatedResult<EmployeesDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
	}
}