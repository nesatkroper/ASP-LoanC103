using ASPLoanMSC103.Data;
using ASPLoanMSC103.Model;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace ASPLoanMSC103.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DapperFactory database;

        public CategoryController(DapperFactory db)
        {
            database = db;
        }

        public IActionResult Index(string? filter)
        {
            var db = database.CreateConnection();

            var sql = @"SELECT * FROM LoanCategories WHERE LoanCategoryCode LIKE '%' + @filter + '%' OR @filter IS NULL;";

            var cats = db.Query<LoanCategory>(sql, new { @filter = filter });
            return View(cats);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LoanCategory? req)
        {
            var db = database.CreateConnection();
            var sql = @"INSERT INTO LoanCategories (LoanCategoryCode, Description, DesInKhmer, Code) VALUES (@LoanCategoryCode, @Description, @DesInKhmer, @Code);";

            int rowEffection = db.Execute(sql, new
            {
                req?.LoanCategoryCode,
                req?.Description,
                req?.DesInKhmer,
                req?.Code
            });
            return rowEffection > 0 ? RedirectToAction(nameof(Index)) : View("Create");
        }

        public IActionResult Edit(int loanCategoryId)
        {
            if (!IsCategoryExist(loanCategoryId)) return NotFound();

            var db = database.CreateConnection();
            string sql = @"SELECT * FROM LoanCategories WHERE LoanCategoryID = @loanCategoryId;";

            var cat = db.QueryFirst<LoanCategory>(sql, new { @loanCategoryId = loanCategoryId });

            return cat is not null ? View("Edit", cat) : NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int loanCategoryId, LoanCategory req)
        {
            if (!IsCategoryExist(loanCategoryId)) return NotFound();

            using var db = database.CreateConnection();
            var sql = @"UPDATE LoanCategories SET LoanCategoryCode = @LoanCategoryCode, Description = @Description, DesInKhmer = @DesInKhmer, Code = @Code, EditSeq = @EditSeq + 1, ModifiedDT = GETDATE(), IsAction = @IsAction WHERE LoanCategoryID = @loanCategoryId;";

            int rowEffection = db.Execute(sql, new
            {
                loanCategoryId,
                req.LoanCategoryCode,
                req.Description,
                req.DesInKhmer,
                req.Code,
                req.IsActive
            });
            return rowEffection > 0 ? RedirectToAction(nameof(Index)) : View("Create");
        }

        [HttpGet]
        public IActionResult Delete(int loanCategoryId)
        {
            if (!IsCategoryExist(loanCategoryId)) return NotFound();

            var db = database.CreateConnection();
            string sql = @"SELECT * FROM LoanCategories WHERE LoanCategoryID = @loanCategoryId;";
            var cat = db.QueryFirst<LoanCategory>(sql, new { @loanCategoryId = loanCategoryId });

            return cat is not null ? View("Delete", cat) : NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int loanCategoryId)
        {
            if (!IsCategoryExist(loanCategoryId)) return NotFound();
            using var db = database.CreateConnection();
            string sql = "DELETE FROM LoanCategories WHERE LoanCategoryID = @loanCategoryId;";

            int deleteRow = db.Execute(sql, new { @loanCategoryId = loanCategoryId });

            return deleteRow > 0 ? RedirectToAction(nameof(Index)) : BadRequest();
        }

        [HttpGet]
        public IActionResult Details(int loanCategoryId)
        {
            if (!IsCategoryExist(loanCategoryId)) return NotFound();
            var db = database.CreateConnection();
            string sql = "SELECT * FROM LoanCategories WHERE LoanCategoryID = @loanCategoryId;";

            var cat = db.QueryFirst<LoanCategory>(sql, new { @loanCategoryId = loanCategoryId });
            return cat is not null ? View("Details", cat) : NotFound();
        }

        private bool IsCategoryExist(int loanCategoryId)
        {
            try
            {
                var db = database.CreateConnection();
                string sql = "SELECT * FROM LoanCategories WHERE LoanCategoryID = @loanCategoryId;";

                var cats = db.Query<LoanCategory>(sql, new { @loanCategoryId = loanCategoryId });
                return cats.Any();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
