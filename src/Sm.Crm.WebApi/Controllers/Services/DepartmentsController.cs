using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Departments.Commands;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class DepartmentsServiceController : ServiceController
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsServiceController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _departmentService.GetAll();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var department = await _departmentService.GetById(id);

        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrUpdateDepartmentDto department)
    {
        await _departmentService.Create(department);

        return Ok(department.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrUpdateDepartmentDto department)
    {
        if (id == department.Id)
        {
            await _departmentService.Update(department);
        }
        return Ok(department.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _departmentService.Delete(id);

        return Ok();
    }
}