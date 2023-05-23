using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using MyProject.Repository.Interface;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyProject.Repository;
using MyProject.Model;
using MyProject.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountRepository accountRepository;
        private string NIK;

        //public AccountController(AccountRepository accountRepository)
        //{
        //    this.accountRepository = accountRepository  ;
        //}

        private readonly EmployeeRepository employeeRepository;
        public IConfiguration _configuration; // add jwt 22-05-2023
        public static Employee employee = new Employee();

        public AccountController(AccountRepository accountRepository, EmployeeRepository employeeRepository, IConfiguration configuration)
        {
            this.accountRepository = accountRepository;
            this.employeeRepository = employeeRepository;
            _configuration = configuration;
        }

        ////Tambahan jwt 22-05-2023
        ////Get untuk login
        //[HttpPost("/Login")]
        //public ActionResult GetLogin(string Email, string Password)
        //{

        //    var get = accountRepository.GetLogin(Email, Password);
        //    var acc = accountRepository.Get().ToList();
        //    var getacc = acc.FirstOrDefault(e => e.Employee.Email == Email);
        //    if (get != null) //bisa pake get.Count() != 0
        //    {
        //        var token = Token(getacc.Employee);
        //        return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Diambil", Data = token });
        //    }
        //    else
        //    {
        //        return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Silahkan Cek Email dan Password. Pastikan Sudah Benar!", Data = get });
        //    }

        //}

        //private string Token(Employee employee)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim("FirstName", employee.FirstName),
        //        new Claim("Lastname", employee.LastName),
        //        new Claim("Email", employee.Email)
        //    };
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
        //        _configuration.GetSection("Jwt:Key").Value));

        //    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: signIn);

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        //    return jwt;

            
        //}

        //-----------------------


        //Get untuk login VM
        [HttpPost("/LoginVM")]
        public ActionResult GetLoginVM(LoginVM loginVM)
        {
            //Tambahan JWT 22-05-2023
            var get = accountRepository.GetLoginVM(loginVM);
            if (get != null) //bisa pake get.Count() != 0
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", loginVM.Email)
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                Console.WriteLine("JWT Token: " + jwtToken);
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Diambil", Token = jwtToken });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Silahkan Cek Email dan Password. Pastikan Sudah Benar!", Data = get });
            }

        }

        [HttpPost("/Register")]
        public ActionResult Register(RegisterVM registerVM)
        {

            var result = accountRepository.Register(registerVM);
            if (result == 0)
            {
                //var get = accountRepository.Get(registerVM);
                return StatusCode(401, new { status = HttpStatusCode.Conflict, message = "Gagal melakukan pendaftaran. Email dan Phone duplikat!", Data = result });
            }
            else if (result == 11)
            {
                return StatusCode(401, new { status = HttpStatusCode.Conflict, message = "Gagal melakukan pendaftaran. Department Tidak Ada!", Data = result });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Di Masukan", Data = result });
            }

        }


        [HttpGet]
        public ActionResult Get()
        {

            var get = accountRepository.Get();
            if (get != null) //bisa pake get.Count() != 0
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Diambil", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan", Data = get });
            }

        }

        [HttpGet("NIK")]
        public ActionResult Get(string NIK)
        {
            var get = accountRepository.Get(NIK);

            if (get == null)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Ditemukan", Data = get });
            }
        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            var hapus = accountRepository.Get(NIK);
            if (hapus == null)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan.", Data = hapus });
            }
            var delete = accountRepository.Delete(NIK);
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Di Hapus" });
        }


        [HttpPut]
        public ActionResult Update(Account account)
        {
            var get = accountRepository.Get(NIK);
            accountRepository.Update(account);
            return Ok();
        }
    }
}










//----
////Get untuk login
//[HttpGet("/Login")]
//public ActionResult GetLogin(string Email, string Password)
//{

//    var get = accountRepository.GetLogin(Email, Password);
//    if (get != null) //bisa pake get.Count() != 0
//    {
//        return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Diambil", Data = get });
//    }
//    else
//    {
//        return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Silahkan Cek Email dan Password. Pastikan Sudah Benar!", Data = get });
//    }

//}


//[HttpPost]
//public ActionResult Insert(Account account)
//{

//    var result = accountRepository.Insert(account);
//    if (result != null)
//    {
//        //// Mengambil data terbaru dari repository
//        var get = accountRepository.Get(account.NIK);
//        return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Di Masukan", Data = result });
//    }
//    //else if (result == 0)
//    //{
//    //    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Sudah Melakukan Register", Data = result });
//    //}
//    else
//    {
//        return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Gagal melakukan pendaftaran", Data = result });
//    }

//}
