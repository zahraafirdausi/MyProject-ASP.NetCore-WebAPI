using Client.Models;
using Client.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Client.Controllers
{
    public class TokenController : Controller
    {
        //private readonly IConfiguration _configuration;
        //private readonly ILogger<TokenController> _logger;

        //public TokenController(ILogger<TokenController> logger, IConfiguration configuration)
        //{
        //    _logger = logger;
        //    _configuration = configuration;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        ////[Authentication]
        ////[Authorize]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Login(string email)
        //{
        //    if (HttpContext.Session.GetString("email") == null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            HttpContext.Session.SetString("email", email.ToString());

        //            var tokenHandler = new JwtSecurityTokenHandler();
        //            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        //            var tokenDescriptor = new SecurityTokenDescriptor
        //            {
        //                Subject = new ClaimsIdentity(new[]
        //                {
        //                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        //                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //                }),
        //                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"])),
        //                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //            };
        //            var token = tokenHandler.CreateToken(tokenDescriptor);
        //            var jwtToken = tokenHandler.WriteToken(token);
        //            //Debug.WriteLine(jwtToken);

        //            //return RedirectToAction("Index", "Departments", new { token = jwtToken });
        //            return RedirectToAction("Index", "Departments", jwtToken); //Gangaruhh
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Departments");
        //    }

        //    return View();
        //}

        //public ActionResult Logout()
        //{
        //    HttpContext.Session.Clear();
        //    HttpContext.Session.Remove("email");

        //    return RedirectToAction("Login");
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}

