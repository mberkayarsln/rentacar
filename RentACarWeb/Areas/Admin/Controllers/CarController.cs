using DataAccessLayer.Data;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RentACarWeb.Areas.Admin.Models.Car;
using RentACarWeb.Languages;

namespace RentACarWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer _stringLocalizer;

        public CarController(ApplicationDbContext context,IStringLocalizer<Lang> stringLocalizer)
        {
            _context = context;
            _stringLocalizer = stringLocalizer;
        }

        public void SetViewBagProperties()
        {
            ViewBag.Make = _stringLocalizer["page.Make"];
            ViewBag.Model = _stringLocalizer["page.Model"];
            ViewBag.Type = _stringLocalizer["page.Type"];
            ViewBag.Year = _stringLocalizer["page.Year"];
            ViewBag.Price = _stringLocalizer["page.Price"];
            ViewBag.Available = _stringLocalizer["page.Available"];
            ViewBag.Operations = _stringLocalizer["page.Operations"];
            ViewBag.Edit = _stringLocalizer["page.Edit"];
            ViewBag.Delete = _stringLocalizer["page.Delete"];
            ViewBag.Create = _stringLocalizer["page.Create"];
            ViewBag.Car = _stringLocalizer["page.Car"];
            ViewBag.DeleteMessage = _stringLocalizer["page.DeleteMessage"];
            //Layout
            ViewBag.HomeLink = _stringLocalizer["page.MainHome"];
            ViewBag.AdminLink = _stringLocalizer["page.MainAdminLink"];
            ViewBag.English = _stringLocalizer["page.English"];
            ViewBag.Turkish = _stringLocalizer["page.Turkish"];
        }

        public IActionResult Index()
        {
            SetViewBagProperties();
            var cars = _context.Cars.Where(c => !c.isDeleted).Select(c => new CarListVM
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Price = c.Price,
                Type = c.Type,
                Year = c.Year,
                IsActive = c.IsActive,
            }).ToList();

            return View(cars);
        }

        public IActionResult Create()
        {
            SetViewBagProperties();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarCreateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagProperties();
                return View(viewModel);
            }

            var car = new Car
            {
                Make = viewModel.Make,
                Price = viewModel.Price,
                Type = viewModel.Type,
                Year = viewModel.Year,
                Model = viewModel.Model,
            };

            _context.Cars.Add(car);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var car = _context.Cars.First(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var viewModel = new CarEditVM
            {
                Id = id,
                Make = car.Make,
                Price = car.Price,
                Type = car.Type,
                Year = car.Year,
                Model = car.Model,
                isActive = car.IsActive,
            };
            SetViewBagProperties();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarEditVM viewModel)
        {

            if (!ModelState.IsValid)
            {
                SetViewBagProperties();
                return View(viewModel);
            }

            var car = new Car
            {
                Id = viewModel.Id,
                Make = viewModel.Make,
                Price = viewModel.Price,
                Type = viewModel.Type,
                Model = viewModel.Model,
                Year = viewModel.Year,
                DateUpdated = DateTime.Now,
                IsActive = viewModel.isActive,
            };

            _context.Update(car);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new { success = false, message = "Invalid ID" });
            }

            var car = _context.Cars.FirstOrDefault(c => c.Id == id);

            if (car == null)
            {
                return Json(new { success = false, message = "Car not found" });
            }

            car.isDeleted = true;
            car.IsActive = false;
            car.DateDeleted = DateTime.Now;
            _context.SaveChanges();
            TempData["message"] = "Car " + car.Make + " " + car.Model + " deleted successfully";
            return Json(new { success = true });
        }
    }
}
