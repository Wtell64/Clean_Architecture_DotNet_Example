using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Tasks.Commands.CreateTask;
public class CreateTask;


public class CreateTaskCommand : IRequest<int>
{
    public int RequestId { get; set; }
    public int EmployeeUserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
    public int TaskStatusId { get; set; }
}

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
{
    private readonly ITaskRepository _repository;
    private readonly IMapper _mapper;

    public CreateTaskCommandHandler(ITaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskItem>(request);
        var id = await _repository.Create(entity);
        return id;
    }
}
