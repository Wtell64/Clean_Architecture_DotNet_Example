using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Sales.Commands.UpdateSale;

public class UpdateSaleCommand : IRequest<bool>
{
    public int Id { get; set; }
    public int RequestId { get; set; }
    public Guid EmployeeUserId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal SaleAmount { get; set; }
    public string Description { get; set; }
}

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Sale>(request);
        bool isSuccess = await _unitOfWork.SaleRepository.Update(entity);
        return isSuccess;
    }
}