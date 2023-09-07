using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class EmployeeAttendanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
