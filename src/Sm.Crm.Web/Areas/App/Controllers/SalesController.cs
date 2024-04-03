using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.Web.Areas.App.Controllers;

public class SalesController : AppController
{
    private readonly ISaleService _saleService;
    //private readonly IEmployeeService _employeeService;
    //private readonly IRequestService _requestService;

    public SalesController(ISaleService saleService
        //,IEmployeeService employeeService,
        //RequestService requestService
        )
    {
        _saleService = saleService;
        //_employeeService = employeeService;
        //_requestService = requestService;
    }

    // GET: SaleController
    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var list = await _saleService.GetPaginatedAsync(new PaginationRequest(page, pageSize));
        return View(list);
    }

    // GET: SaleController/View/5
    public async Task<PartialViewResult> View(int id)
    {
        var dto = await _saleService.GetByIdAsync(id);
        return PartialView("_View", dto.Data);
    }

    // GET: SaleController/Create
    public async Task<PartialViewResult> Add()
    {
        var dto = new CreateOrEditSaleDto();

        await FillDropdownItems();

        return PartialView("_Form", dto);
    }

    // POST: SaleController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Add(CreateOrEditSaleDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _saleService.CreateAsync(dto);

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

    // GET: SaleController/Edit/5
    public async Task<PartialViewResult> Edit(int id)
    {
        var dto = await _saleService.GetByIdAsync(id);

        await FillDropdownItems();

        return PartialView("_Form", dto.Data);
    }

    // POST: SaleController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrEditSaleDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _saleService.UpdateAsync(dto);
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

    // GET: SaleController/Delete/5
    public async Task<PartialViewResult> Delete(int id)
    {
        var dto = await _saleService.GetByIdAsync(id);
        return PartialView("_Delete", dto);
    }

    // POST: SaleController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteItem(int id)
    {
        await _saleService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task FillDropdownItems()
    {
        //ViewBag.Employees = (await _employeeService.GetAll())
        //    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
        //    .ToList();
        //ViewBag.Requests = (await _requestService.GetAll())
        //    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
        //    .ToList();
    }
}