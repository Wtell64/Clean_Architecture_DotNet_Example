using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Customers.Commands.CreateCustomer;
using Sm.Crm.Application.Features.Customers.Commands.DeleteCustomer;
using Sm.Crm.Application.Features.Customers.Commands.UpdateCustomer;
using Sm.Crm.Application.Features.Customers.Queries.GetAllCustomers;

namespace Sm.Crm.WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPaginatedCustomersQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var response = await _mediator.Send(new GetCustomerByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCustomerCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, UpdateCustomerCommand command)
    {
        if (id == command.Id)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        return Ok(false);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await _mediator.Send(new DeleteCustomerCommand(id));
        return Ok(response);
    }
}