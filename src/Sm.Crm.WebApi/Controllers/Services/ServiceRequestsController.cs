using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class RequestsServiceController : ServiceController
{
    private readonly IRequestService _requestService;

    public RequestsServiceController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _requestService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _requestService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditRequestDto userAddress)
    {
        await _requestService.Create(userAddress);
        return Ok(userAddress.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditRequestDto userAddress)
    {
        if (id == userAddress.Id)
        {
            await _requestService.Update(userAddress);
        }
        return Ok(userAddress.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _requestService.Delete(id);

        return Ok();
    }
}