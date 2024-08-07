using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentACarWeb.Areas.Admin.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
    }
}
