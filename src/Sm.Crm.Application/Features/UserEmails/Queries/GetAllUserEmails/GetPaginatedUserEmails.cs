using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Features.UserEmails.Queries;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.UserEmails.Queries.GetAllUserEmails;

public class GetPaginatedUserEmailsQuery : IRequest<PaginatedResult<UserEmailDto>>
{
    public string? Search { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedUserEmailsQueryHandler : IRequestHandler<GetPaginatedUserEmailsQuery, PaginatedResult<UserEmailDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedUserEmailsQueryHandler(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<UserEmailDto>> Handle(GetPaginatedUserEmailsQuery request, CancellationToken cancellationToken)
    {
        var entities = _db.UserEmails
             .Include(e => e.UserFk)
             .OrderByDescending(e => e.Id)
             .ProjectTo<UserEmailDto>(_mapper.ConfigurationProvider);

        if (!string.IsNullOrEmpty(request.Search))
        {
            entities = entities.Where(e =>
                e.EmailAddress.Contains(request.Search)
              
             );
        }

        return await PaginatedResult<UserEmailDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}