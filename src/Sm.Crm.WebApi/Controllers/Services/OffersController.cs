using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class OffersServiceController : ServiceController
{
    private readonly IOfferService _offerService;

    public OffersServiceController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _offerService.GetAll();

        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var data = await _offerService.GetById(id);

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditOfferDto dto)
    {
        await _offerService.Create(dto);

        return Ok(dto.RequestId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditOfferDto dto)
    {
        if (id == dto.RequestId)
        {
            await _offerService.Update(dto);
        }
        return Ok(dto.RequestId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _offerService.Delete(id);

        return Ok();
    }
}