using AutoMapper;
using MediatR;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.UserAddresses.Queries.GetByIdUserAddresses;
public class GetByIdUserAddressesQuery:IRequest<UserAddressDto?>
{
    public int Id { get; set; }
    public GetByIdUserAddressesQuery(int  id)
    {
        Id = id;
    }
}
public class GetByIdUserAddressesQueryHandler : IRequestHandler<GetByIdUserAddressesQuery, UserAddressDto?>
{
    readonly IUserAddressRepository _repository;
    readonly IMapper _mapper;

    public GetByIdUserAddressesQueryHandler(IUserAddressRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserAddressDto?> Handle(GetByIdUserAddressesQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);        
        return _mapper.Map<UserAddressDto>(entity);
    }
}
