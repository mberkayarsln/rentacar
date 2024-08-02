using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RentACarWeb.Languages;
using RentACarWeb.Models;
using System.Diagnostics;

namespace RentACarWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly IStringLocalizer<Lang> _stringLocalizer;

        public HomeController(IStringLocalizer<Lang> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public void SetViewBagProperties()
        {
            ViewBag.PageHome = _stringLocalizer["page.MainHome"];
            ViewBag.PageText = _stringLocalizer["page.MainText"];

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
