using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.Web.Areas.App.Controllers;

public class EmployeesController : AppController
{
    // TODO: Diğer servisler ve seed veriler tamamlandığında tekrardan düzenlenmesi gerekecek. Hem buranın hemde Views klasörünün.

    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var list = await _employeeService.GetPaginated(new PaginationRequest(page, pageSize));
        return View(list);
    }

    public async Task<PartialViewResult> View(int id)
    {
        var dto = await _employeeService.GetById(id);
        return PartialView("_View", dto.Data);
    }

    [HttpGet]
    public async Task<PartialViewResult> Add()
    {
        var dto = new CreateOrUpdateEmployeeDto();

        //await FillDropdownItems();

        return PartialView("_Form", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Add(CreateOrUpdateEmployeeDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _employeeService.Create(dto);

                return Json(new { IsSuccess = true, RedirectUrl = Url.Action(nameof(Index)) });
            }
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Unable to save changes!");
        }

        //await FillDropdownItems();

        return PartialView("_Form", dto);
    }

    [HttpGet]
    public async Task<PartialViewResult> Edit(int id)
    {
        var dto = await _employeeService.GetById(id);

        //await FillDropdownItems();

        return PartialView("_Form", dto.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrUpdateEmployeeDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _employeeService.Update(dto);
                return Json(new { IsSuccess = true, RedirectUrl = Url.Action(nameof(Index)) });
            }
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Unable to save changes!");
        }

        //await FillDropdownItems();

        return PartialView("_Form", dto);
    }

    [HttpGet]
    public async Task<PartialViewResult> Delete(int id)
    {
        var dto = await _employeeService.GetById(id);
        return PartialView("_Delete", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteItem(int id)
    {
        await _employeeService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task FillDropdownItems()
    {
        //ViewBag.Titles = (await _titleService.GetAll())
        //	.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
        //	.ToList();
    }
}