using AutoMapper;
using MediatR;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Features.RequestStatuses.Queries.GetRequestStatusById;
public class GetRequestStatusByIdQuery : IRequest<RequestStatusDto?>
{
    public int Id { get; set; }

    public GetRequestStatusByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetRequestStatusByIdQueryHandler : IRequestHandler<GetRequestStatusByIdQuery, RequestStatusDto?>
{
    private readonly IRequestStatusRepository _repository;
    private readonly IMapper _mapper;

    public GetRequestStatusByIdQueryHandler(IRequestStatusRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RequestStatusDto?> Handle(GetRequestStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        return _mapper.Map<RequestStatusDto>(entity);
    }
}
