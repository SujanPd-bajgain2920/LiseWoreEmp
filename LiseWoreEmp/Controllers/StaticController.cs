using Microsoft.AspNetCore.Mvc;

namespace LiseWoreEmp.Controllers
{
    public class StaticController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
