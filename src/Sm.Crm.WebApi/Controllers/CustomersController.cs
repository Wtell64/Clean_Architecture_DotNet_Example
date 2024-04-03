using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Customers.Commands.CreateCustomer;
using Sm.Crm.Application.Features.UserEmails.Commands.Delete;
using Sm.Crm.Application.Features.UserEmails.Commands.UpdateEmails;
using Sm.Crm.Application.Features.UserEmails.Queries.GetAllUserEmails;
using Sm.Crm.Application.Features.UserEmails.Queries.GetUserEmailsById;

namespace Sm.Crm.WebApi.Controllers;

public class CustomersController : FeatureController
{
    private readonly IMediator _mediator;

    public UserEmailsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPaginatedUserEmailsQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var response = await _mediator.Send(new GetUserEmailByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateUserEmailCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateUserEmailCommand command)
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
        var response = await _mediator.Send(new DeleteUserEmailCommand(id));
        return Ok(response);
    }
}