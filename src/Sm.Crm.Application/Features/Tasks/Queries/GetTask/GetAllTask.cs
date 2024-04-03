using AutoMapper;
using MediatR;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Tasks.Queries.GetTask;

public record GetAllTasksQuery : IRequest<ICollection<TaskDto>>;

public class GetAllCustomerQueryHandler : IRequestHandler<GetAllTasksQuery, ICollection<TaskDto>>
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

    public async Task<ICollection<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var entities =  _unitOfWork.TaskRepository.GetAll();
        return _mapper.Map<List<TaskDto>>(entities).ToList();
    }
}
