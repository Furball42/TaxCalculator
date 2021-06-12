using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaxCalculator.MVC.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public ActionResult Details(decimal annualIncome, string postalCode)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:5000/");
        //        var response = client.GetAsync("Calculation/DoTaxCalculation");

        //        var result = response.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            //var readTask = result.Content.ReadAsAsync<IList<StudentViewModel>>();
        //            //readTask.Wait();

        //            //students = readTask.Result;
        //        }
        //        else //web api sent error response 
        //        {
        //            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
        //        }
        //    }

        //    //var webApi = new TaxCalculator.API.Controllers.CalculationController();
        //    //ViewBag.Message = webApi.mysearch("test string"); // Change ViewBag.Message to something for your controller
        //    //return View();

        //    return View();
        //}
    }
}
