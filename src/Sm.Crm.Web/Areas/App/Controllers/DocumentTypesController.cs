using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;

namespace Sm.Crm.Web.Areas.App.Controllers;

public class DocumentTypesController : AppController
{
    private readonly IDocumentTypeService _documentTypeService;

    public DocumentTypesController(IDocumentTypeService documentTypeService)
    {
        _documentTypeService = documentTypeService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var list = await _documentTypeService.GetPaginated(new PaginationRequest(page, pageSize));
        return View(list);
    }

    public async Task<PartialViewResult> View(int id)
    {
        var dto = await _documentTypeService.GetById(id);
        return PartialView("_View", dto.Data);
    }

    [HttpGet]
    public async Task<PartialViewResult> Add()
    {
        var dto = new CreateOrUpdateCustomerDto();

        return PartialView("_Form", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Add(CreateOrEditDocumentTypeDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _documentTypeService.Create(dto);

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
        var dto = await _documentTypeService.GetFormById(id);

        return PartialView("_Form", dto.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrEditDocumentTypeDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _documentTypeService.Update(dto);
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
        var dto = await _documentTypeService.GetById(id);
        return PartialView("_Delete", dto.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteItem(int id)
    {
        await _documentTypeService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}