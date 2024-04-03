using Microsoft.AspNetCore.Mvc;

namespace Sm.Crm.Web.Areas.App.Controllers;

public class HomeController : AppController
{
    public IActionResult Index()
    {
        return View();
    }
}