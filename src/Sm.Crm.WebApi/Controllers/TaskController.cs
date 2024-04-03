using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Tasks.Commands.CreateTask;
using Sm.Crm.Application.Features.Tasks.Commands.DeleteTask;
using Sm.Crm.Application.Features.Tasks.Commands.UpdateTask;
using Sm.Crm.Application.Features.Tasks.Queries.GetTask;
using Sm.Crm.Application.Features.Tasks.Queries.GetTaskById;

namespace Sm.Crm.WebApi.Controllers;

public class TaskController : FeatureController
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPaginatedTasksQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetTaskByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateTaskCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, UpdateTaskCommand command)
    {
        if (id == command.Id)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        return Ok(false);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteTaskCommand(id));
        return Ok(response);
    }
}
