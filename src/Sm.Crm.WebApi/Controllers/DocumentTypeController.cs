using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.DocumentTypes.Commands.CreateDocumentType;
using Sm.Crm.Application.Features.DocumentTypes.Commands.DeleteDocumentType;
using Sm.Crm.Application.Features.DocumentTypes.Commands.UpdateDocumentType;
using Sm.Crm.Application.Features.DocumentTypes.Queries.GetDocumentType;
using Sm.Crm.Application.Features.DocumentTypes.Queries.GetDocumentTypeById;

namespace Sm.Crm.WebApi.Controllers;

public class DocumentTypeController : FeatureController
{
    private readonly IMediator _mediator;
    public DocumentTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetAllDocumentTypeQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetDocumentTypeByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateDocumentTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateDocumentTypeCommand command)
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
        var response = await _mediator.Send(new DeleteDocumentTypeCommand(id));
        return Ok(response);
    }
}
