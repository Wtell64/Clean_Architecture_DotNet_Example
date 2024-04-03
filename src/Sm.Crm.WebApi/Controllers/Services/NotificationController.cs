using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.WebApi.Controllers.Services;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationServiceController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationServiceController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var notification = await _notificationService.GetAll();

        return Ok(notification);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var notification = await _notificationService.GetById(id);

        return Ok(notification);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrEditNotificationDto notification)
    {
        await _notificationService.Create(notification);

        return Ok(notification.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrEditNotificationDto notification)
    {
        if (id == notification.Id)
        {
            await _notificationService.Update(notification);
        }
        return Ok(notification.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _notificationService.Delete(id);

        return Ok();
    }
}
