using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Requests.Queries.GetRequestById;
public record GetRequestByIdQuery(int Id) : IRequest<RequestDto?>;

public class GetRequestByIdQueryHandler : IRequestHandler<GetRequestByIdQuery, RequestDto>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IMapper _mapper;

    public GetRequestByIdQueryHandler(IRequestRepository requestRepository, IMapper mapper)
    {
        _requestRepository = requestRepository;
        _mapper = mapper;
    }

    public async Task<RequestDto> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var entity =await _requestRepository.GetAll()
            .Include(e => e.CustomerFk).ThenInclude(e => e.UserFk)
            .Include(e => e.EmployeeFk).ThenInclude(e => e.UserFk)
            .Include(e => e.RequestStatusFk)
            .FirstOrDefaultAsync(e => e.Id.Equals(request.Id));

        return _mapper.Map<RequestDto>(entity);
    }
}