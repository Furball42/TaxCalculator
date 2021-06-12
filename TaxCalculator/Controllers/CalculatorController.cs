using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Models;

namespace TaxCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            var model = new CalculatorViewModel();
            model.PostalCode = "ASDFASDFADSFA";
            return View(model);
        }
    }
}
