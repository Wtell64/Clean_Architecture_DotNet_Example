using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Requests.Queries.GetRequest;
public class GetPaginatedRequestsQuery : IRequest<PaginatedResult<RequestDto>>
{
    public string? Search { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedRequestsQueryHandler : IRequestHandler<GetPaginatedRequestsQuery, PaginatedResult<RequestDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPaginatedRequestsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<RequestDto>> Handle(GetPaginatedRequestsQuery request, CancellationToken cancellationToken)
    {
        var entities = _context.Requests
            .Include(e => e.CustomerFk).ThenInclude(e => e.UserFk)
            .Include(e => e.EmployeeFk).ThenInclude(e => e.UserFk)
            .Include(e => e.RequestStatusFk)
            .OrderByDescending(e => e.Id)
            .ProjectTo<RequestDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<RequestDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}