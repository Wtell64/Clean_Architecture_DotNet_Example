using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class SalesServiceController : ServiceController
{
    private readonly ISaleService _saleService;

    public SalesServiceController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    // GET: api/<SaleController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var sale = await _saleService.GetAllAsync();

        return Ok(sale);
    }

    // GET api/<SaleController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var sale = await _saleService.GetByIdAsync(id);

        return Ok(sale);
    }

    // POST api/<SaleController>
    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditSaleDto sale)
    {
        await _saleService.CreateAsync(sale);

        return Ok(sale.Id);
    }

    // PUT api/<SaleController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditSaleDto sale)
    {
        if (id == sale.Id)
        {
            await _saleService.UpdateAsync(sale);
        }
        return Ok(sale.Id);
    }

    // DELETE api/<SaleController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _saleService.DeleteAsync(id);

        return Ok();
    }
}