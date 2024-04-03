using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class CustomersServiceController : ServiceController
{
    private readonly ICustomerService _customerService;

    public CustomersServiceController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _customerService.GetAll();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var customer = await _customerService.GetById(id);

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrUpdateCustomerDto customer)
    {
        await _customerService.Create(customer);

        return Ok(customer.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrUpdateCustomerDto customer)
    {
        if (id == customer.Id)
        {
            await _customerService.Update(customer);
        }
        return Ok(customer.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _customerService.Delete(id);

        return Ok();
    }
}