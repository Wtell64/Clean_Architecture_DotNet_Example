using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.Web.Areas.App.Controllers;

public class RequestsController : AppController
{
    private readonly IRequestService _requestService;
    private readonly ICustomerService _customerService;

    public RequestsController(IRequestService requestService, ICustomerService customerService)
    {
        _requestService = requestService;
        _customerService = customerService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var list = await _requestService.GetPaginated(new PaginationRequest(page, pageSize));
        return View(list);
    }

    public async Task<PartialViewResult> View(int id)
    {
        var dto = await _requestService.GetById(id);
        return PartialView("_View", dto.Data);
    }

    [HttpGet]
    public async Task<PartialViewResult> Add()
    {
        var dto = new CreateOrEditRequestDto();

        await FillDropdownItems();

        return PartialView("_Form", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Add(CreateOrEditRequestDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _requestService.Create(dto);

                return Json(new { IsSuccess = true, RedirectUrl = Url.Action(nameof(Index)) });
            }
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Unable to save changes.");
        }

        await FillDropdownItems();
        return PartialView("_Form", dto);
    }

    public async Task<PartialViewResult> Edit(int id)
    {
        var dto = await _requestService.GetFormById(id);

        await FillDropdownItems();

        return PartialView("_Form", dto.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrEditRequestDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _requestService.Update(dto);

                return Json(new { IsSuccess = true, RedirectUrl = Url.Action(nameof(Index)) });
            }
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Unable to save changes.");
        }

        return PartialView("_Form", dto);
    }

    [HttpGet]
    public async Task<PartialViewResult> Delete(int id)
    {
        var dto = await _requestService.GetById(id);
        return PartialView("_Delete", dto.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteItem(int id)
    {
        await _requestService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task FillDropdownItems()
    {
        var result = await _customerService.GetAll();
        if (result.Data != null)
        {
            ViewBag.Customer = result.Data
                .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.FullName })
                .ToList();
        }
        else
        {
            ViewBag.Customer = new List<SelectListItem>();
        }
    }
}