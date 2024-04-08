using Lab10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Consultation consultation)
        {
            if (consultation.Subject.Equals("Basics") && consultation.Date.DayOfWeek == DayOfWeek.Monday)
                ModelState.AddModelError("", "Констультація з теми Основи не може проходити по понеділках!");
            if (consultation.Date <= DateTime.Now)
                ModelState.AddModelError("Date", "Дата консультації має бути у майбутньому!");
            if (consultation.Date.DayOfWeek == DayOfWeek.Sunday || consultation.Date.DayOfWeek == DayOfWeek.Saturday)
                ModelState.AddModelError("Date", "Консультації на вихідних недоступні!");
            if (ModelState.IsValid)
                return View("Success", consultation);
            else
            {
                foreach (var item in ModelState)
                {
                    if (item.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                        foreach (var error in item.Value.Errors)
                            consultation.Errors.Add($"{error.ErrorMessage}");
                }
                return View(consultation);
            }

        }

        [HttpGet]
        public IActionResult Confirm(Consultation consultation)
        {
            return View(consultation);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

