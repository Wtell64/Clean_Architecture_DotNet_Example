using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Tasks.Queries.GetTask;

public class GetPaginatedTasksQuery : IRequest<PaginatedResult<TaskDto>>
{
    public string? Search { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedTasksQueryHandler : IRequestHandler<GetPaginatedTasksQuery, PaginatedResult<TaskDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedTasksQueryHandler(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<TaskDto>> Handle(GetPaginatedTasksQuery request, CancellationToken cancellationToken)
    {
        var entities = _db.Tasks
             .OrderByDescending(e => e.Id)
             .ProjectTo<TaskDto>(_mapper.ConfigurationProvider);

        if (!string.IsNullOrEmpty(request.Search))
        {
            entities = entities.Where(e =>
                e.Description.Contains(request.Search)
             );
        }

        return await PaginatedResult<TaskDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}
