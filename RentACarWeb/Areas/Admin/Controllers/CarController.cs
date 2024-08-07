using DataAccessLayer.Data;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RentACarWeb.Areas.Admin.Models.Car;
using RentACarWeb.Languages;

namespace RentACarWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : BaseController
    {

        private readonly ApplicationDbContext _context;

        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarCreateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
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

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarEditVM viewModel)
        {

            if (!ModelState.IsValid)
            {
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
