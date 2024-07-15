using DotnetProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace DotnetProject.Controllers
{
    public class CurrencyController : Controller
    {

        public IActionResult Index()
        {
            List<string> liste = new List<string>() { "USD", "EUR", "GBP" };
            ViewBag.CurrencyName = new SelectList(liste);
            return View();
        }
    }
}



