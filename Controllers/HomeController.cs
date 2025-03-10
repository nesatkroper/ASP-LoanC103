using Microsoft.AspNetCore.Mvc;

namespace ASPLoanMSC103.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return RedirectToAction("Login", "Account");
    }
  }
}
