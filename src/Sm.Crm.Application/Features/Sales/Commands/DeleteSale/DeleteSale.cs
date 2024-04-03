using MediatR;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Sales.Commands.DeleteSale;

public record DeleteSaleCommand(int Id) : IRequest<bool>;

public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSaleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        bool isSuccess = await _unitOfWork.SaleRepository.DeleteById(request.Id);
        return isSuccess;
    }
}