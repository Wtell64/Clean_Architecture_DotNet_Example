using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Features.RequestStatuses.Queries.GetRequestStatus;
public class GetPaginatedRequestStatusQuery : IRequest<PaginatedResult<RequestStatusDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaginatedRequestStatusQueryHandler : IRequestHandler<GetPaginatedRequestStatusQuery, PaginatedResult<RequestStatusDto>>
{
    private readonly IRequestStatusRepository _repository;
    private readonly IMapper _mapper;

    public GetPaginatedRequestStatusQueryHandler(IRequestStatusRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<RequestStatusDto>> Handle(GetPaginatedRequestStatusQuery request, CancellationToken cancellationToken)
    {
        var entities = _repository.GetAll()
             .OrderByDescending(e => e.Id)
             .ProjectTo<RequestStatusDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<RequestStatusDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }
}
