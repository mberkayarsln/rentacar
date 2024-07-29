using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using RentACarWeb.Areas.Admin.Models.Rental;

namespace RentACarWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RentalController : Controller
    {

        private readonly ApplicationDbContext _context;

        public RentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
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
            var viewModel = new RentalCreateVM();

            viewModel.CustomerCar.Cars = _context.Cars.Where(c => !c.isDeleted).ToList();
            viewModel.CustomerCar.Customers = _context.Customers.Where(c => !c.isDeleted).ToList();

            return View(viewModel);
        }
    }
}
