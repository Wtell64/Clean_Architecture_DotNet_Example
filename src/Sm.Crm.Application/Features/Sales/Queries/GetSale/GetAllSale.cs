using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Sales.Queries.GetSale;
public record GetAllSalesQuery : IRequest<ICollection<SaleDto>>;

public class GetAllSaleQueryHandler : IRequestHandler<GetAllSalesQuery, ICollection<SaleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllSaleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ICollection<SaleDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.SaleRepository.GetAll().ToListAsync();
        return _mapper.Map<List<SaleDto>>(entities).ToList();
    }
}