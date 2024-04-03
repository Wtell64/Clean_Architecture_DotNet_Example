using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers;

public class UserEmailsServiceController : ServiceController
{
    private readonly IUserEmailService _userEmailService;

    public UserEmailsServiceController(IUserEmailService userEmailService)
    {
        _userEmailService = userEmailService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _userEmailService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _userEmailService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditUserEmailDto userEmail)
    {
        await _userEmailService.Create(userEmail);
        return Ok(userEmail.UserId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditUserEmailDto userEmail)
    {
        if (id == userEmail.UserId)
        {
            await _userEmailService.Update(userEmail);
        }
        return Ok(userEmail.UserId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userEmailService.Delete(id);

        return Ok();
    }
}