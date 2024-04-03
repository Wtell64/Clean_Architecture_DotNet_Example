using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Web.Areas.App.Controllers;

public class StatusTypesController : AppController
{
    private readonly IStatusTypeService _statusTypeService;
    private readonly IStatusTypeRepository _statusTypeRepository;

    public StatusTypesController(IStatusTypeService statusTypeService, IStatusTypeRepository statusTypeRepository)
    {
        _statusTypeService = statusTypeService;
        _statusTypeRepository = statusTypeRepository;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var list = await _statusTypeService.GetPaginated(new PaginationRequest(page, pageSize));

        return View(list);
    }

    public async Task<PartialViewResult> View(int id)
    {
        var dto = await _statusTypeService.GetById(id);
        return PartialView("_View", dto.Data);
    }

    [HttpGet]
    public async Task<PartialViewResult> Add()
    {
        var dto = new CreateOrEditStatusTypeDto();

        await FillDropdownItems();

        return PartialView("_Form", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Add(CreateOrEditStatusTypeDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _statusTypeService.Create(dto);

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
        var dto = await _statusTypeService.GetById(id);

        await FillDropdownItems();

        return PartialView("_Form", dto.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrEditStatusTypeDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _statusTypeService.Update(dto);
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
        var dto = await _statusTypeService.GetById(id);
        return PartialView("_Delete", dto.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteItem(int id)
    {
        await _statusTypeService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task FillDropdownItems()
    {
        ViewBag.StatusTypes = (await _statusTypeService.GetAll()).Data?
            .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
            .ToList();
    }
}