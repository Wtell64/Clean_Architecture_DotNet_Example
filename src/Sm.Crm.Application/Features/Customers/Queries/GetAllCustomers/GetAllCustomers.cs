using AutoMapper;
using MediatR;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Customers.Queries.GetAllCustomers;

// IRequest<T>: Dönüş tipi
public record GetAllCustomersQuery : IRequest<ICollection<CustomerDto>>;

//public class GetAllCustomersQuery : IRequest<ICollection<CustomerDto>>
//{
//    // Metot parametreleri
//    // Bu metotta parametre yok
//}

public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomersQuery, ICollection<CustomerDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCustomerQueryHandler(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ICollection<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.CustomerRepository.GetAllWithUser();
        return _mapper.Map<List<CustomerDto>>(entities).ToList();
    }
}