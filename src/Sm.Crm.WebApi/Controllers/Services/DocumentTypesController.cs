using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class DocumentTypesServiceController : ServiceController
{
    private readonly IDocumentTypeService _documentTypeService;

    public DocumentTypesServiceController(IDocumentTypeService documentTypeService)
    {
        _documentTypeService = documentTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _documentTypeService.GetAll();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var customer = await _documentTypeService.GetById(id);

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditDocumentTypeDto customer)
    {
        await _documentTypeService.Create(customer);

        return Ok(customer.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditDocumentTypeDto customer)
    {
        if (id == customer.Id)
        {
            await _documentTypeService.Update(customer);
        }
        return Ok(customer.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _documentTypeService.Delete(id);

        return Ok();
    }
}