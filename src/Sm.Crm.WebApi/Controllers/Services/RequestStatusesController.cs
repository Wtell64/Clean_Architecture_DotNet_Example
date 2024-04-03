using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class RequestStatusesServiceController : ServiceController
{
    private readonly IRequestStatusService _requestStatusService;

    public RequestStatusesServiceController(IRequestStatusService requestStatusService)
    {
        _requestStatusService = requestStatusService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _requestStatusService.GetAll();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var customer = await _requestStatusService.GetById(id);

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditRequestStatusDto requeststatus)
    {
        await _requestStatusService.Create(requeststatus);

        return Ok(requeststatus.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditRequestStatusDto requeststatus)
    {
        if (id == requeststatus.Id)
        {
            await _requestStatusService.Update(requeststatus);
        }
        return Ok(requeststatus.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _requestStatusService.Delete(id);

        return Ok();
    }
}