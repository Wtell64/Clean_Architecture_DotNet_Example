using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class TitlesServiceController : ServiceController
{
    private readonly ITitleService _titleService;

    public TitlesServiceController(ITitleService titleService)
    {
        _titleService = titleService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var titles = await _titleService.GetAll();

        return Ok(titles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var title = await _titleService.GetById(id);

        return Ok(title);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditTitleDto title)
    {
        await _titleService.Create(title);

        return Ok(title.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditTitleDto title)
    {
        if (id == title.Id)
        {
            await _titleService.Update(title);
        }
        return Ok(title.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _titleService.Delete(id);

        return Ok();
    }
}