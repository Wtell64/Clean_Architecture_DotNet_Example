using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Sales.Queries.GetSaleById;

public record GetSaleByIdQuery(int Id) : IRequest<SaleDto?>;

public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, SaleDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSaleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SaleDto?> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.SaleRepository.GetAll().FirstOrDefaultAsync(e => e.Id == request.Id);
        return _mapper.Map<SaleDto>(entity);
    }
}