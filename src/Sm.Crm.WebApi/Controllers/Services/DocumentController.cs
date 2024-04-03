using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class DocumentsServiceController : ServiceController
{
    private readonly IDocumentService _documentService;

    public DocumentsServiceController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _documentService.GetAll();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var customer = await _documentService.GetById(id);

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditDocumentDto dto)
    {
        await _documentService.Create(dto);

        return Ok(dto.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditDocumentDto dto)
    {
        if (id == dto.Id)
        {
            await _documentService.Update(dto);
        }
        return Ok(dto.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _documentService.Delete(id);

        return Ok();
    }
}