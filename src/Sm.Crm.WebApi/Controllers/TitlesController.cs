using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Titles.Commands.CreateTitle;
using Sm.Crm.Application.Features.Titles.Commands.DeleteTitle;
using Sm.Crm.Application.Features.Titles.Commands.UpdateTitle;
using Sm.Crm.Application.Features.Titles.Queries.GetAllTitles;

namespace Sm.Crm.WebApi.Controllers;

public class TitlesController : FeatureController
{
    private readonly IMediator _mediator;

    public TitlesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetAllTitlesQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetTitleByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateTitleCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateTitleCommand command)
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
        var response = await _mediator.Send(new DeleteTitleCommand(id));
        return Ok(response);
    }
}