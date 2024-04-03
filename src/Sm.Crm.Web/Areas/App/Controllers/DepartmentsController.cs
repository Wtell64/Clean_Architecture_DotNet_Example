using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Features.Departments.Commands;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.Web.Areas.App.Controllers;

public class DepartmentsController : AppController
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var list = await _departmentService.GetPaginated(new PaginationRequest(page, pageSize));
        return View(list);
    }

    public async Task<PartialViewResult> View(int id)
    {
        var dto = await _departmentService.GetById(id);
        return PartialView("_View", dto.Data);
    }

    [HttpGet]
    public async Task<PartialViewResult> Add()
    {
        var dto = new CreateOrUpdateDepartmentDto();

        return PartialView("_Form", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Add(CreateOrUpdateDepartmentDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Create(dto);

                return Json(new { IsSuccess = true, RedirectUrl = Url.Action(nameof(Index)) });
            }
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Unable to save changes!");
        }

        return PartialView("_Form", dto);
    }

    public async Task<PartialViewResult> Edit(int id)
    {
        var dto = await _departmentService.GetFormById(id);

        return PartialView("_Form", dto.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrUpdateDepartmentDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Update(dto);
                return Json(new { IsSuccess = true, RedirectUrl = Url.Action(nameof(Index)) });
            }
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Unable to save changes!");
        }

        return PartialView("_Form", dto);
    }

    [HttpGet]
    public async Task<PartialViewResult> Delete(int id)
    {
        var dto = await _departmentService.GetById(id);
        return PartialView("_Delete", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteItem(int id)
    {
        await _departmentService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}