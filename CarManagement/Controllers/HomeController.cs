using Microsoft.AspNetCore.Mvc;

namespace CarManagement.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
