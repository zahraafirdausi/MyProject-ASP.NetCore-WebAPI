using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Model;
using MyProject.Repository;
using MyProject.Context;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using MyProject.Repository.Interface;
using System.Linq;
//using Microsoft.AspNetCore.Cors;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        private string NIK;

        public EmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public ActionResult Insert(Employee employee)
        {

            var result = employeeRepository.Insert(employee);
            if (result != 0)
            {
                //// Mengambil data terbaru dari repository
                var get = employeeRepository.Get(employee.NIK);
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Di Masukan", Data = result });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Nomor HP atau Email sudah Terdaftar", Data = result });
            }

            //-----------------------------------------
            //if (employee.Department != null && employee.Department.Id != 0)
            //{
            //    var department = employeeRepository.Get_DepartmentID(employee.Department.Id);
            //    //var department = employeeRepository.Get_DepartmentID(department.Id);
            //    if (department != null)
            //    {
            //        employee.Department = department;
            //    }
            //}
            //-----------------------------------------
            //// Memasukkan data ke repository
            //int result = employeeRepository.Insert(employee);
            //if (result != 0)
            //{
            //    //// Mengambil data terbaru dari repository
            //    var get = employeeRepository.Get();
            //    return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Di Masukan", Data = result });
            //}
            //else
            //{
            //    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Nomor HP atau Email sudah Terdaftar", Data = result });
        }
        //----------------------------------------

        //Get beberapa row
        [HttpGet("/Rows")]
        public ActionResult GetRows()
        {

            var get = employeeRepository.GetRows();
            if (get != null) //bisa pake get.Count() != 0
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Diambil", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan", Data = get });
            }
            //--------------------------------------
            //var getdept = employeeRepository.Get_Department();

        }

        //tambahan view
        //------------
        //[EnableCors("AllowOrigin")] //tambahan
        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS Berhasil");
        }
        //------------



        [HttpGet]
        public ActionResult Get()
        {

            var get = employeeRepository.Get();
            if (get != null) //bisa pake get.Count() != 0
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Diambil", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan", Data = get });
            }
            //--------------------------------------
            //var getdept = employeeRepository.Get_Department();

        }

        [HttpGet("NIK")]
        public ActionResult Get(string NIK)
        {
            var get = employeeRepository.Get(NIK);

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
            var getd = employeeRepository.Get(NIK);
            if (getd == null)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan.", Data = getd });
            }
            var delete = employeeRepository.Delete(NIK);
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Di Hapus" });
        }

        //[HttpPut("{NIK}")]
        [HttpPut]
        public ActionResult Update(Employee employee)
        {
            if (employee.Department != null && employee.Department.Id != 0)
            {
                var department = employeeRepository.Get_DepartmentID(employee.Department.Id);
                if (department != null)
                {
                    employee.Department = department;

                }
            }
            employeeRepository.Update(employee);
            return Ok();
            //----------------------
            //var department = employeeRepository.Get(employee.NIK);
            //if (department.Department != null)
            //{
            //    employeeRepository.Update(employee);
            //    return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Di Update" });

            //}
            //return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "ID Department Tidak Ditemukan. Gagal Update Data!" });
            //-----------------------
            //var get = employeeRepository.Get(NIK);
            //employeeRepository.Update(employee);
            //return Ok();

        }

    }
}


//-------------
//insert
//// Memasukkan data ke repository
//employeeRepository.Insert(employee);
//// Mengambil data terbaru dari repository
//var get = employeeRepository.Get();
//// Mengirimkan respons dengan data yang baru ditambahkan
//return Ok($"Status : 200 \nMessage : Data berhasil ditambahkan \nData : {get}");
//--------------

//var get = employeeRepository.Get().Select(e => new
//{
//    NIK = e.NIK,
//    FirstName = e.FirstName,
//    LastName = e.LastName,
//    Phone = e.Phone,
//    BirthDate = e.BirthDate,
//    Salary = e.Salary,
//    Email = e.Email,
//    Gender = e.Gender.ToString(),
//    DepartmentId = e.Department,
//    DepartmentName = e.Department
//    //DepartmentId = e.Department != null ? e.Department.Id : 0, // Jika departemen tidak null, ambil ID departemen, jika null, return 0
//    //DepartmentName = e.Department != null ? e.Department.Name : "" // Jika departemen tidak null, ambil nama departemen, jika null, return empty string
//}).ToList();
//return Ok(new { status = HttpStatusCode.OK, message = "Data Berhasil Diambil", Data = get });

//------------------------------------