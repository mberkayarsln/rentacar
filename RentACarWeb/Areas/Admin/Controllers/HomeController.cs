using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RentACarWeb.Languages;

namespace RentACarWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<Lang> _stringLocalizer;

        public HomeController(IStringLocalizer<Lang> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public void SetViewBagProperties()
        {
            ViewBag.Customer = _stringLocalizer["page.Customer"];
            ViewBag.Car = _stringLocalizer["page.Car"];
            ViewBag.Rental = _stringLocalizer["page.Rental"];

            //Layout
            ViewBag.HomeLink = _stringLocalizer["page.MainHome"];
            ViewBag.AdminLink = _stringLocalizer["page.MainAdminLink"];
            ViewBag.English = _stringLocalizer["page.English"];
            ViewBag.Turkish = _stringLocalizer["page.Turkish"];
        }

        public IActionResult Index()
        {
            SetViewBagProperties();
            return View();
        }
    }
}
