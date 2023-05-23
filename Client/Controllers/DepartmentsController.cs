using Client.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    //[Authorize] //add 21-05-2023
    public class DepartmentsController : Controller
    {
        //[Authentication] // Hapus Selasa 23-05-2023
        //[AllowAnonymous] //Untuk memastikan bahwa pengguna tidak dapat mengakses halaman Index Departemen setelah logout. add 21-05-2023
        //[HttpGet] //add 21-05-2023
        public IActionResult Index()
        {
            return View();
        }
    }
}
