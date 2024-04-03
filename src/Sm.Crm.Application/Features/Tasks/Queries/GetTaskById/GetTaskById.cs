using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Common;

namespace Sm.Crm.Application.Features.Tasks.Queries.GetTaskById;
public class GetTaskByIdQuery : IRequest<TaskDto?>
{
    public long Id { get; set; }

    public GetTaskByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto?>
{
    private readonly IApplicationDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTaskByIdQueryHandler(IApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.TaskRepository
            .GetAll()
            .FirstOrDefaultAsync(e => e.Id == request.Id);
        return _mapper.Map<TaskDto>(entity);
    }
}
