using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Customers.Queries.GetAllCustomers;

public class GetPaginatedCustomersQuery : IRequest<PaginatedResult<CustomerDto>>
{
    public string? Search { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedCustomersQueryHandler : IRequestHandler<GetPaginatedCustomersQuery, PaginatedResult<CustomerDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedCustomersQueryHandler(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<CustomerDto>> Handle(GetPaginatedCustomersQuery request, CancellationToken cancellationToken)
    {
        var entities = _db.Customers
             .Include(e => e.UserFk)
             .Include(e => e.TitleFk)
             .OrderByDescending(e => e.Id)
             .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider);

        if (!string.IsNullOrEmpty(request.Search))
        {
            entities = entities.Where(e =>
                e.CompanyName.Contains(request.Search) ||
                e.TitleName.Contains(request.Search)
             );
        }

        return await PaginatedResult<CustomerDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}