using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Sales.Commands.CreateSale;
using Sm.Crm.Application.Features.Sales.Commands.DeleteSale;
using Sm.Crm.Application.Features.Sales.Commands.UpdateSale;
using Sm.Crm.Application.Features.Sales.Queries.GetSale;
using Sm.Crm.Application.Features.Sales.Queries.GetSaleById;

namespace Sm.Crm.WebApi.Controllers;

public class SalesController : FeatureController
{
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPaginatedSaleQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetSaleByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateSaleCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateSaleCommand command)
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
        var response = await _mediator.Send(new DeleteSaleCommand(id));
        return Ok(response);
    }
}