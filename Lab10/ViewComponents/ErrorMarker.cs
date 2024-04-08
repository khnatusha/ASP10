using Lab10.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab10.ViewComponents
{
    public class ErrorMarker : ViewComponent
    {
        public IViewComponentResult? Invoke(Consultation consultation)
        {
            if (consultation == null)
                return View("Valid");
            if (consultation.Errors.Count == 0)
                return View("Valid");
            return View(consultation.Errors);
        }
    }
}

