using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Constants;
using Sm.Crm.Domain.Enums;
using System;

namespace Sm.Crm.Web.Areas.App.Controllers;

[Authorize(Roles = Roles.Administrator + "," + Roles.CustomerManager)] //VEYA
public class CustomersController : AppController
{
    private readonly ICustomerService _customerService;
    private readonly ITitleService _titleService;
    private readonly IStatusTypeService _statusTypeService;

    public CustomersController(ICustomerService customerService, ITitleService titleService, IStatusTypeService statusTypeService)
    {
        _customerService = customerService;
        _titleService = titleService;
        _statusTypeService = statusTypeService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var list = await _customerService.GetPaginated(new PaginationRequest(page, pageSize));
        return View(list);
    }

    public async Task<PartialViewResult> View(long id)
    {
        var dto = await _customerService.GetById(id);
        return PartialView("_View", dto.Data);
    }

    [HttpGet]
    public async Task<PartialViewResult> Add()
    {
        var dto = new CreateOrUpdateCustomerDto();

        await FillDropdownItems();

        return PartialView("_Form", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Add(CreateOrUpdateCustomerDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _customerService.Create(dto);

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

    public async Task<PartialViewResult> Edit(long id)
    {
        var dto = await _customerService.GetFormById(id);

        await FillDropdownItems();

        return PartialView("_Form", dto.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrUpdateCustomerDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _customerService.Update(dto);
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
    public async Task<PartialViewResult> Delete(long id)
    {
        var dto = await _customerService.GetById(id);
        return PartialView("_Delete", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteItem(long id)
    {
        await _customerService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task FillDropdownItems()
    {
        ViewBag.Titles = (await _titleService.GetAll()).Data?
            .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
            .ToList();

        ViewBag.StatusTypes = (await _statusTypeService.GetAll()).Data?
            .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
            .ToList();
        
        ViewBag.CustomerTypes = EnumToSelectList<CustomerTypeEnum>();
    }
}