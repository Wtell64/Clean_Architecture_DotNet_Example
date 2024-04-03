using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sm.Crm.Web.Areas.App.Controllers;

[Authorize]
[Area("App")]
public abstract class AppController : Controller
{
    public static IEnumerable<SelectListItem> EnumToSelectList<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>()
            .Select(e => new SelectListItem() { Text = e.ToString(), Value = e.ToString() })
            .ToList();
    }
}