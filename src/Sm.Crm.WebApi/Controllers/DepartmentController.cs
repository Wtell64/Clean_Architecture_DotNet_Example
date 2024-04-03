using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Departments.Commands.CreateDepartment;
using Sm.Crm.Application.Features.Departments.Commands.DeleteDepartment;
using Sm.Crm.Application.Features.Departments.Commands.UpdateDepartment;
using Sm.Crm.Application.Features.Departments.Queries.GetDepartment;
using Sm.Crm.Application.Features.Departments.Queries.GetDepartmentById;

namespace Sm.Crm.WebApi.Controllers;

public class DepartmentController : FeatureController
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPaginatedDepartmentQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetDepartmentById(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateDepartmentCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateDepartmentCommand command)
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
        var response = await _mediator.Send(new DeleteDepartmentCommand(id));
        return Ok(response);
    }
}