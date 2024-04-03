using AutoMapper;
using MediatR;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Tasks.Commands.UpdateTask;
public class UpdateTaskCommand : IRequest<bool>
{
    public int? Id { get; set; }
    public int RequestId { get; set; }
    public int EmployeeUserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
    public int TaskStatusId { get; set; }
}

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskItem>(request);
        bool isSuccess = await _taskRepository.Update(entity);
        return isSuccess;
    }
}
