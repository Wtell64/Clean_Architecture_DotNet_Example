using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class UserAddressesServiceController : ServiceController
{
    private readonly IUserAddressService _userAddressService;

    public UserAddressesServiceController(IUserAddressService userAddressService)
    {
        _userAddressService = userAddressService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _userAddressService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _userAddressService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditUserAddressDto userAddress)
    {
        await _userAddressService.Create(userAddress);
        return Ok(userAddress.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditUserAddressDto userAddress)
    {
        if (id == userAddress.Id)
        {
            await _userAddressService.Update(userAddress);
        }
        return Ok(userAddress.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userAddressService.Delete(id);

        return Ok();
    }
}