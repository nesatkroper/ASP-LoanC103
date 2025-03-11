using ASPLoanMSC103.Data;
using ASPLoanMSC103.Model;
using Microsoft.AspNetCore.Mvc;

namespace ASPLoanMSC103.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Department/
        public IActionResult Index()
        {
            var dept = _context.Departments.ToList();
            return View(dept);
        }

        // GET: /Department/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department dep)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(dep);
                int insert = _context.SaveChanges();
                if (insert > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to save the department. Please try again.");
            }
            return View(dep);
        }

        // GET: /Department/Edit/1
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        // POST: /Department/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ID, DepartmentName, Description")] Department dept)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Update(dept);
                int insert = _context.SaveChanges();
                if (insert > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to update the department. Please try again.");
            }
            return View(dept);
        }

        // GET: /Department/Details/1
        [HttpGet]
        public IActionResult Details(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        // GET: /Department/Delete/1
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        // POST: /Department/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept == null) return NotFound();

            _context.Departments.Remove(dept);
            int insert = _context.SaveChanges();
            if (insert > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Failed to delete the department. Please try again.");
            return View(dept);
        }
    }
}