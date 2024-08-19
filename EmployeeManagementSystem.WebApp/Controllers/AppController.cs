using EmployeeManagementSystem.WebApp.AppFilter;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.WebApp.Controllers
{
    [AppFilterController]
    public class AppController : Controller
    {
        // GET: AppController
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult Employee()
        {
            return View();
        }
        public IActionResult Department()
        {
            return View();
        }

    }
}
