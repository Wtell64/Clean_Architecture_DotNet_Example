using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.UserAddresses.Queries.GetAllUserAddresses;
public class GetPaginationUserAddressesQuery:IRequest<PaginatedResult<UserAddressDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetPaginationUserAddressesQueryHandler : IRequestHandler<GetPaginationUserAddressesQuery, PaginatedResult<UserAddressDto>>
{
    readonly IUserAddressRepository _repository;
    readonly IMapper _mapper;

    public GetPaginationUserAddressesQueryHandler(IUserAddressRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<UserAddressDto>> Handle(GetPaginationUserAddressesQuery request, CancellationToken cancellationToken)
    {
        var entities =  _repository.GetAll().OrderByDescending(u => u.Id).ProjectTo<UserAddressDto>(_mapper.ConfigurationProvider);
        return await PaginatedResult<UserAddressDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}
