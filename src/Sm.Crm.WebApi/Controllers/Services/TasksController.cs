using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class TasksServiceController : ServiceController
{
    private readonly ITaskService _taskService;

    public TasksServiceController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entity = await _taskService.GetAll();

        return Ok(entity);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _taskService.GetById(id);

        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditTaskDto dto)
    {
        await _taskService.Create(dto);

        return Ok(dto.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditTaskDto dto)
    {
        if (id == dto.Id)
        {
            await _taskService.Update(dto);
        }
        return Ok(dto.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _taskService.Delete(id);

        return Ok();
    }
}