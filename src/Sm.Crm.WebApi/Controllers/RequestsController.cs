using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Requests.Commands.CreateRequest;
using Sm.Crm.Application.Features.Requests.Commands.DeleteRequest;
using Sm.Crm.Application.Features.Requests.Commands.UpdateRequest;
using Sm.Crm.Application.Features.Requests.Queries.GetRequest;
using Sm.Crm.Application.Features.Requests.Queries.GetRequestById;

namespace Sm.Crm.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPaginatedRequestsQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetRequestByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateRequestCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateRequestCommand command)
    {
        if (id.Equals(command.Id))
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        return Ok(false);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteRequestCommand(id));
        return Ok(response);
    }
}