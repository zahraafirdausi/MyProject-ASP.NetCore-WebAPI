using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
