

using ASPLoanMSC103.Model;
using ASPLoanMSC103.Data;
using ASPLoanMSC103.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPLoanMSC103.Services;
using ASPLoanMSC103.Extensions;



namespace ASPLoanMSC103.Controllers
{
    [Authorize]
    public sealed class LoanController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILoanServices _loanServices;

        public LoanController(ILoanServices loanServices, AppDbContext context)
        {
            _loanServices = loanServices;
            _context = context;
        }



        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<DtResponse<LoanViewModel>>> Index([FromBody] DtRequest request)
        {
            var dtResult = await _loanServices.Gets(request);
            return Ok(dtResult);
        }

        public IActionResult Create(int customerId)
        {
            var categories = _context.LoanCategories.Select(c => new
            {
                CategoryId = c.LoanCategoryID,
                Name = c.Description
            }).ToList();

            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            if (customerId > 0)
            {
                var customers = _context.Customers
                .Where(x => x.CustomerId.Equals(customerId))
                .AsNoTracking().Select(c => new CustomerRequest(c.CustomerId, $"{c.FirstName}{c.LastName}")).ToList();

                if (customers.Any())
                {
                    ViewBag.Customers = new SelectList(customers, "CustomerId", "CustomerName");
                    ViewBag.CustomerId = customerId;
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Loan req)
        {
            if (!ModelState.IsValid)
            {
                return View(req);
            }

            _loanServices.CreateLoan(req);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Preview(Loan req)
        {
            var loanSchedules = new LoanCalculator(req.Principle, req.InterestRate, req.Installment);
            var schedules = LoanCalculatorExtensions.CreateSchedule(loanSchedules);
            return PartialView(schedules);
        }

        [HttpGet]
        [ActionName("Schedule")]
        public async Task<IActionResult> ShowSchedule(int id)
        {
            var loaWithSchedule = await _loanServices.GetLoanWithSchedules(id);
            return View("Schedule", loaWithSchedule);
        }

        [HttpGet]
        [ActionName("PaymentSchedule")]
        public async Task<IActionResult> PaymentSchedule(int id, int scheduleId)
        {
            var loaWithSchedule = await _loanServices.GetSchedulePaymentById(id, scheduleId);
            return PartialView("RecieveModal", loaWithSchedule);
        }
        public async Task<IActionResult> Details(long id)
        {
            var loan = await _loanServices.GetLoanWithSchedules(id);
            return View(loan);
        }

        public IActionResult GetCustomers(string term)
        {
            IQueryable<Customer> queries = _context.Customers;

            if (!string.IsNullOrEmpty(term))
            {
                var data = queries.Where(a => a.FirstName!.Contains(term) ||
                a.FirstName!.Contains(term))
                .AsNoTracking()
                .Select(c => new CustomerRequest(c.CustomerId, $"{c.FirstName}{c.LastName}"));

                return Ok(data);
            }
            return Ok();
        }

        public IActionResult GetEmployees(string term)
        {
            IQueryable<Employee> queries = _context.Employees;

            if (!string.IsNullOrEmpty(term))
            {
                var data = queries.Where(a => a.FirstName!.Contains(term) ||
                a.FirstName!.Contains(term)).AsNoTracking().Select(c => new EmployeeRequest(c.EmployeeId, $"{c.FirstName}{c.LastName}"));

                return Ok(data);
            }
            return Ok();
        }
    }
}