using Client.Models;
using Client.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Client.Controllers
{
    public class AccountController : Controller
    {

        private readonly IConfiguration _configuration;
        //private readonly ILogger<AccountController> _logger;

        public AccountController(IConfiguration configuration)
        {
            //_logger = logger;
            _configuration = configuration;
        }

        //[HttpPost]
        //public IActionResult Login(string email)
        //{
        //    if (HttpContext.Session.GetString("email") == null)
        //    {
        //        var claims = new[] {
        //                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //                new Claim("Email", email)
        //            };
        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //        var token = new JwtSecurityToken(
        //            _configuration["Jwt:Issuer"],
        //            _configuration["Jwt:Audience"],
        //            claims,
        //            expires: DateTime.UtcNow.AddMinutes(10),
        //            //expires: DateTime.UtcNow.AddSeconds(15),
        //            signingCredentials: signIn);

        //        var cookieOptions = new CookieOptions
        //        {
        //            Expires = DateTime.UtcNow.AddMinutes(10),
        //            HttpOnly = true,
        //            SameSite = SameSiteMode.None,
        //            Secure = true
        //        };

        //        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        //        Console.WriteLine("JWT Token: " + jwtToken);
        //        HttpContext.Response.Cookies.Append("jwt", new JwtSecurityTokenHandler().WriteToken(token), cookieOptions);
        //        HttpContext.Session.SetString("email", email.ToString());
        //        return Ok(new { Token = jwtToken });
        //        //return RedirectToAction("Index", "Department", new { token = jwtToken });

        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}



        public IActionResult Login()
        {

            //if (HttpContext.Session.GetString("email") == null)
            //{
                return View();
            //}
            //else
            //{
                //return RedirectToAction("Index", "Departments");
            //}
        }

        //Post Action // login menggunakan session
        //[HttpPost]
        //public ActionResult Login(string email)
        //{
        //    if (HttpContext.Session.GetString("email") == null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            HttpContext.Session.SetString("email", email.ToString());
        //            return RedirectToAction("Index", "Departments");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    return View();
        //}
        //}

        //public ActionResult Logout()
        //{

        //    HttpContext.Session.Clear();
        //    HttpContext.Session.Remove("email");

        //    return RedirectToAction("Login");
        //}

        //[Authentication]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}