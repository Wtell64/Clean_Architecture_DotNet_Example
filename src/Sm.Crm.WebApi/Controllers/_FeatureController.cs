using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sm.Crm.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FeatureController : ControllerBase
{
}
