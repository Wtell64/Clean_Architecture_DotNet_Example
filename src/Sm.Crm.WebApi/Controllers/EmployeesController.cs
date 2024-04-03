using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Employees.Commands.CreateEmployee;
using Sm.Crm.Application.Features.Employees.Commands.DeleteEmployee;
using Sm.Crm.Application.Features.Employees.Commands.UpdateEmployee;
using Sm.Crm.Application.Features.Employees.Queries.GetAllEmployees;
using Sm.Crm.Application.Features.Employees.Queries.GetEmployeeById;

namespace Sm.Crm.WebApi.Controllers;

public class EmployeesController : FeatureController
{
	private readonly IMediator _mediator;

	public EmployeesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] GetPaginatedEmployeesQuery query)
	{
		var response = await _mediator.Send(query);
		return Ok(response);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var response = await _mediator.Send(new GetEmployeeByIdQuery(id));
		return Ok(response);
	}

	[HttpPost]
	public async Task<IActionResult> Post(CreateEmployeeCommand command)
	{
		var response = await _mediator.Send(command);
		return Ok(response);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Put(int id, UpdateEmployeeCommand command)
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
		var response = await _mediator.Send(new DeleteEmployeeCommand(id));
		return Ok(response);
	}
}
