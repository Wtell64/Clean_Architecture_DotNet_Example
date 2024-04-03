using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class EmployeesServiceController : ServiceController
{
    private readonly IEmployeeService _employeeService;

    public EmployeesServiceController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var employees = await _employeeService.GetAll();

        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var employee = await _employeeService.GetById(id);

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrUpdateEmployeeDto employee)
    {
        await _employeeService.Create(employee);

        return Ok(employee.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrUpdateEmployeeDto employee)
    {
        if (id == employee.Id)
        {
            await _employeeService.Update(employee);
        }
        return Ok(employee.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _employeeService.Delete(id);

        return Ok();
    }
}