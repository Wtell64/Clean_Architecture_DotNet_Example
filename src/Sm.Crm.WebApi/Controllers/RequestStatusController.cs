using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.RequestStatuses.Commands.CreateRequestStatus;
using Sm.Crm.Application.Features.RequestStatuses.Commands.DeleteRequestStatus;
using Sm.Crm.Application.Features.RequestStatuses.Commands.UpdateRequestStatus;
using Sm.Crm.Application.Features.RequestStatuses.Queries.GetRequestStatus;
using Sm.Crm.Application.Features.RequestStatuses.Queries.GetRequestStatusById;

namespace Sm.Crm.WebApi.Controllers;

public class RequestStatusController : FeatureController
{
    private readonly IMediator _mediator;

    public RequestStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetAllRequestStatusQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetRequestStatusByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateRequestStatusCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateRequestStatusCommand command)
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
        var response = await _mediator.Send(new DeleteRequestStatusCommand(id));
        return Ok(response);
    }
}
