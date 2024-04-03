using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class StatusTypesServiceController : ServiceController
{
    private readonly IStatusTypeService _statusTypeService;

    public StatusTypesServiceController(IStatusTypeService statusTypeService)
    {
        _statusTypeService = statusTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _statusTypeService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _statusTypeService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditStatusTypeDto statusType)
    {
        await _statusTypeService.Create(statusType);
        return Ok(statusType.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditStatusTypeDto statusType)
    {
        if (id == statusType.Id)
        {
            await _statusTypeService.Update(statusType);
        }
        return Ok(statusType.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _statusTypeService.Delete(id);

        return Ok();
    }
}