using Microsoft.AspNetCore.Mvc;

namespace TaxCalculator.MVC.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}