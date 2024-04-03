using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.UserAddresses.Queries.GetAllUserAddresses;
public record GetAllUserAddreses:IRequest<ICollection<UserAddressDto>>;
public class GetAllUserAddressesHandler : IRequestHandler<GetAllUserAddreses, ICollection<UserAddressDto>>
{
    readonly IUserAddressRepository _repository;
    readonly IUnitOfWork _unitOfWork;
    readonly IMapper _mapper;

    public GetAllUserAddressesHandler(IUserAddressRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ICollection<UserAddressDto>> Handle(GetAllUserAddreses request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAll().ToListAsync(cancellationToken);
        return _mapper.Map<List<UserAddressDto>>(entities);
    }
}
