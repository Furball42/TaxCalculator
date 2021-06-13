using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.MVC.Controllers
{
    public class PostalCodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
