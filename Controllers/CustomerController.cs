
using ASPLoanMSC103.Data;
using ASPLoanMSC103.Model;
using Microsoft.AspNetCore.Mvc;

namespace ASPLoanMSC103.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        { _context = context; }

        public IActionResult Index()
        {
            var cus = _context.Customers.ToList();
            return View(cus);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer cus)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(cus);
                int insert = _context.SaveChanges();
                return insert > 0 ? RedirectToAction(nameof(Index)) : View();
            }
            {
                ModelState.AddModelError("", "Failed to save the Customer. Please try again.");
            }
            return View(cus);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cus = _context.Customers.Find(id);
            if (cus == null)
            {
                return NotFound();
            }
            return PartialView("_Edit", cus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer cus)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Update(cus);
                int update = _context.SaveChanges();
                return update > 0 ? RedirectToAction(nameof(Index)) : View();
            }
            {
                ModelState.AddModelError("", "Failed to update the Customer. Please try again.");
            }
            return View(cus);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var cus = _context.Customers.Find(id);
            if (cus == null)
            {
                return NotFound();
            }
            return PartialView("_Detail", cus);
        }

        public IActionResult Delete(int id)
        {
            var cus = _context.Customers.Find(id);
            if (cus == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(cus);
            int delete = _context.SaveChanges();
            return delete > 0 ? RedirectToAction(nameof(Index)) : View();
        }
    }
}