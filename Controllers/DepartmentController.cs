using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASPLoanMSC103.Data;
using ASPLoanMSC103.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASPLoanMSC103.Controllers
{
    [Route("[controller]")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var dept = _context.Departments.ToList();
            return View(dept);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ActionName([Bind("DepartmentName", "Description")] Department dept)
        {
            _context.Departments.Add(dept);
            int insert = _context.SaveChanges();
            return insert > 0 ? RedirectToAction(nameof(Index)) : View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept is null) return NotFound();
            return View("Edit", dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eidt(Department dept)
        {
            _context.Departments.Update(dept);
            int insert = _context.SaveChanges();
            return insert > 0 ? RedirectToAction(nameof(Index)) : View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept is null) return NotFound();
            return View("Details", dept);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept is null) return NotFound();
            return View("Delete", dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept is null) return NotFound();

            _context.Departments.Remove(dept);
            int insert = _context.SaveChanges();
            return insert > 0 ? RedirectToAction(nameof(Index)) : View();
        }
    }
}