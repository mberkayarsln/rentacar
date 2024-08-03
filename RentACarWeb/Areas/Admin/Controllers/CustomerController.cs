using DataAccessLayer.Data;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RentACarWeb.Areas.Admin.Models.Customer;
using RentACarWeb.Languages;

namespace RentACarWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers = _context.Customers.Where(c => !c.isDeleted).Select(c => new CustomerListVM
            {
                Id = c.Id,
                FullName = c.FirstName + " " + c.LastName,
                Budget = c.Budget,
                PhoneNumber = c.PhoneNumber,
                isActive = c.isActive,
            }).ToList();

            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerCreateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var customer = new Customer
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Budget = viewModel.Budget,
                PhoneNumber = viewModel.PhoneNumber,
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();
            TempData["message"] = "Customer " + customer.FirstName + " " + customer.LastName + " created successfully.";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerEditVM
            {
                Id = id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Budget = customer.Budget,
                PhoneNumber = customer.PhoneNumber,
                isActive = customer.isActive,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CustomerEditVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var customer = new Customer
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Budget = viewModel.Budget,
                PhoneNumber = viewModel.PhoneNumber,
                isActive = viewModel.isActive,
                DateUpdated = DateTime.Now
            };

            _context.Update(customer);
            _context.SaveChanges();
            TempData["message"] = "Customer " + customer.FirstName + " " + customer.LastName + " updated succesfully";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new { success = false, message = "Invalid id" });
            }

            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return Json(new { success = false, message = "Customer not found" });
            }

            customer.isDeleted = true;
            customer.isActive = false;
            customer.DateDeleted = DateTime.Now;
            _context.SaveChanges();
            TempData["message"] = "Customer " + customer.FirstName + " " + customer.LastName + " deleted successfully";
            return Json(new { success = true });
        }
    }
}
