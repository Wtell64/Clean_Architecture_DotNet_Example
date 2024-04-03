using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.Web.Areas.App.Controllers;

public class NotificationController : Controller
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var list = await _notificationService.GetPaginated(new PaginationRequest(page, pageSize));

        return View(list);
    }

    public async Task<PartialViewResult> View(int id)
    {
        var dto = await _notificationService.GetById(id);
        return PartialView("_View", dto.Data);
    }

    [HttpGet]
    public async Task<PartialViewResult> Add()
    {
        var dto = new CreateOrEditNotificationDto();

        await FillDropdownItems();

        return PartialView("_Form", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Add(CreateOrEditNotificationDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _notificationService.Create(dto);

                return Json(new { IsSuccess = true, RedirectUrl = Url.Action(nameof(Index)) });
            }
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Unable to save changes!");
        }

        await FillDropdownItems();

        return PartialView("_Form", dto);
    }

    public async Task<PartialViewResult> Edit(int id)
    {
        var dto = await _notificationService.GetById(id);               /* GetFormById(id);*/

        await FillDropdownItems();

        return PartialView("_Form", dto.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrEditNotificationDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _notificationService.Update(dto);
                return Json(new { IsSuccess = true, RedirectUrl = Url.Action(nameof(Index)) });
            }
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Unable to save changes!");
        }

        await FillDropdownItems();

        return PartialView("_Form", dto);
    }

    [HttpGet]
    public async Task<PartialViewResult> Delete(int id)
    {
        var dto = await _notificationService.GetById(id);
        return PartialView("_Delete", dto.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteItem(int id)
    {
        await _notificationService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task FillDropdownItems()
    {
    }
}
