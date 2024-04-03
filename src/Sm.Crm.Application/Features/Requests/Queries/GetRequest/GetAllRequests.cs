using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Requests.Queries.GetRequest;
public class GetAllRequestsQuery : IRequest<ICollection<RequestDto>>;

public class GetAllRequestsQeuryHandler : IRequestHandler<GetAllRequestsQuery, ICollection<RequestDto>>
{

    private readonly IRequestRepository _requestRepository;
    private readonly IMapper _mapper;

    public GetAllRequestsQeuryHandler(IRequestRepository requestRepository, IMapper mapper)
    {
        _requestRepository = requestRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<RequestDto>> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
    {
        var entities = _requestRepository.GetAll()
            .Include(e => e.CustomerFk).ThenInclude(e => e.UserFk)
            .Include(e => e.EmployeeFk).ThenInclude(e => e.UserFk)
            .Include(e => e.RequestStatusFk);

        var list = await entities.ToListAsync();

        return _mapper.Map<List<RequestDto>>(list);
    }
}