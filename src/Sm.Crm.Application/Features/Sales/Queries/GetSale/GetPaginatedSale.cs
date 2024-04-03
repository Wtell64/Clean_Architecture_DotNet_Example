using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Sales.Queries.GetSale;

public class GetPaginatedSaleQuery : IRequest<PaginatedResult<SaleDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedSaleQueryHandler : IRequestHandler<GetPaginatedSaleQuery, PaginatedResult<SaleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedSaleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<SaleDto>> Handle(GetPaginatedSaleQuery request, CancellationToken cancellationToken)
    {
        var entities = _unitOfWork.SaleRepository.GetAll()
            //.Include(e => e.EmployeeUserFk)
            .OrderByDescending(e => e.Id)
            .ProjectTo<SaleDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<SaleDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}