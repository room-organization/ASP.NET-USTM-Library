using Microsoft.AspNetCore.Mvc;

namespace USTM_Library.Controllers
{
    public class LandingPageController : Controller
    {
        public IActionResult LandigPage()
        {
            return View();
        }
    }
}
