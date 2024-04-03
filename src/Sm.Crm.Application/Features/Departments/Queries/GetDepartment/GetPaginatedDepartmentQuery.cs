using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Departments.Queries.GetDepartment;

public class GetPaginatedDepartmentQuery : IRequest<PaginatedResult<DepartmentDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedDepartmentQueryHandler : IRequestHandler<GetPaginatedDepartmentQuery, PaginatedResult<DepartmentDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetPaginatedDepartmentQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<DepartmentDto>> Handle(GetPaginatedDepartmentQuery request, CancellationToken cancellationToken)
    {
        var entities = _db.Departments
             .OrderByDescending(e => e.Id)
             .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<DepartmentDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}