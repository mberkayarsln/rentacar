using DataAccessLayer.Data;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RentACarWeb.Areas.Admin.Models.Rental;
using RentACarWeb.Languages;

namespace RentACarWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RentalController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<Lang> _stringLocalizer;

        public RentalController(ApplicationDbContext context,IStringLocalizer<Lang> stringLocalizer)
        {
            _context = context;
            _stringLocalizer = stringLocalizer;
        }

        public void SetViewBagProperties()
        {
            ViewBag.Rental = _stringLocalizer["page.Rental"];
            ViewBag.Customer = _stringLocalizer["page.Customer"];
            ViewBag.Car = _stringLocalizer["page.Car"];
            ViewBag.StartDate = _stringLocalizer["page.StartDate"];
            ViewBag.EndDate = _stringLocalizer["page.EndDate"];
            ViewBag.Payment = _stringLocalizer["page.Payment"];
            ViewBag.Operations = _stringLocalizer["page.Operations"];
            ViewBag.Edit = _stringLocalizer["page.Edit"];
            ViewBag.Delete = _stringLocalizer["page.Delete"];
            ViewBag.Create = _stringLocalizer["page.Create"];
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

            var rentals = _context.Rentals.Where(r => !r.isDeleted).Select(r => new RentalListVM
            {
                Id = r.Id,
                Customer = r.Customer,
                Car = r.Car,
                EndingDate = r.EndingDate,
                StartingDate = r.StartingDate,
                Payment = r.Payment,
            }).ToList();


            return View(rentals);
        }

        public IActionResult Create()
        {
            SetViewBagProperties();

            var viewModel = new RentalCreateVM();

            viewModel.CustomerCar.Cars = _context.Cars.Where(c => !c.isDeleted).ToList();
            viewModel.CustomerCar.Customers = _context.Customers.Where(c => !c.isDeleted).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RentalCreateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagProperties();

                return View(viewModel);
            }

            var rental = new Rental
            {
                CarId = viewModel.CarId,
                CustomerId = viewModel.CustomerId,
                StartingDate = viewModel.StartingDate,
                EndingDate = viewModel.EndingDate,
            };

            var car = _context.Cars.FirstOrDefault(c => c.Id == rental.CarId);
            var dateDiff = (rental.EndingDate - rental.StartingDate).Days;

            if (car != null)
            {
                var payment = car.Price * dateDiff;
                rental.Payment = payment;
            }

            _context.Rentals.Add(rental);
            _context.SaveChanges();

            TempData["message"] = "Rental created successfully";
            return RedirectToAction("Index");

        }


        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var rental = _context.Rentals.Where(r => !r.isDeleted).FirstOrDefault(c => c.Id == id);

            if (rental == null)
            {
                return NotFound();
            }

            var viewModel = new RentalEditVM
            {
                Id = rental.Id,
                CarId = rental.CarId,
                CustomerId = rental.CustomerId,
                StartingDate = rental.StartingDate,
                EndingDate = rental.EndingDate,
            };

            viewModel.CustomerCar.Cars = _context.Cars.Where(c => !c.isDeleted).ToList();
            viewModel.CustomerCar.Customers = _context.Customers.Where(c => !c.isDeleted).ToList();

            SetViewBagProperties();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RentalEditVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagProperties();

                return View(viewModel);
            }

            var rental = new Rental
            {
                Id = viewModel.Id,
                CarId = viewModel.CarId,
                CustomerId = viewModel.CustomerId,
                StartingDate = viewModel.StartingDate,
                EndingDate = viewModel.EndingDate,
                DateUpdated = DateTime.Now
            };

            var car = _context.Cars.FirstOrDefault(c => c.Id == rental.CarId);
            var dateDiff = (rental.EndingDate - rental.StartingDate).Days;

            if (car != null)
            {
                var payment = car.Price * dateDiff;
                rental.Payment = payment;
            }

            _context.Update(rental);
            _context.SaveChanges();
            TempData["message"] = "Rental updated succesfully";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new { success = false, message = "Invalid ID" });
            }

            var rental = _context.Rentals.Where(r => !r.isDeleted).FirstOrDefault(r => r.Id == id);

            if (rental == null)
            {
                return Json(new { success = false, message = "Rental not found" });
            }

            rental.isDeleted = true;
            rental.DateDeleted = DateTime.Now;
            _context.SaveChanges();
            TempData["message"] = "Rental deleted successfully";
            return Json(new { success = true });
        }
    }
}
