
using ASPLoanMSC103.Data;
using ASPLoanMSC103.Model;
using Microsoft.AspNetCore.Mvc;


namespace ASPLoanMSC103.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        { _context = context; }

        public IActionResult Index()
        {
            var emp = _context.Employees.ToList();
            return View(emp);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(emp);
                int insert = _context.SaveChanges();
                return insert > 0 ? RedirectToAction(nameof(Index)) : View();
            }
            else
            {
                ModelState.AddModelError("", "Failed to save the employee. Please try again.");
            }
            return View(emp);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }
            return PartialView("_Edit", emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(emp);
                int update = _context.SaveChanges();
                return update > 0 ? RedirectToAction(nameof(Index)) : View();
            }
            {
                ModelState.AddModelError("", "Failed to update the employee. Please try again.");
            }
            return View(emp);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }
            return PartialView("_Detail", emp);
        }

        public IActionResult Delete(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(emp);
            int delete = _context.SaveChanges();
            return delete > 0 ? RedirectToAction(nameof(Index)) : View();
        }
    }
}