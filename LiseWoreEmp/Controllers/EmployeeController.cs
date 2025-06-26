using Microsoft.AspNetCore.Mvc;

namespace LiseWoreEmp.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
