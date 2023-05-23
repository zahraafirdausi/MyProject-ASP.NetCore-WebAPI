//using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    //[EnableCors("AllowOrigin")] //tambahan
    public class TestCorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
