using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Customers.Queries.GetAllCustomers;

// Burada Request ve Response tiplerini ayarlıyoruz.
// IRequest<T>: Dönüş tipi
public class GetCustomerByIdQuery : IRequest<CustomerDto?>
{
    // Metot parametreleri
    public long Id { get; set; }

    public GetCustomerByIdQuery(long id)
    {
        Id = id;
    }
}
 
// Handler Sınıfları IRequest'i yani Query'i kullanarak yapmak isteğimizi işi tanımlar.
public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.CustomerRepository
            .GetAll(e => e.TitleFk, e => e.StatusTypeFk)
            .FirstOrDefaultAsync(e => e.Id == request.Id);
        return _mapper.Map<CustomerDto>(entity);
    }
}