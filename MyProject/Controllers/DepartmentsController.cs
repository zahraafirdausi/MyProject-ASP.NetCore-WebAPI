using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Model;
using MyProject.Repository;
using MyProject.Repository.Interface;
using System.Net;

namespace MyProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentRepository departmentRepository;
        //private readonly EmployeeRepository employeeRepository;
        private int Id;

        public DepartmentsController(DepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        //public EmployeesController(EmployeeRepository employeeRepository)
        //{
        //    this.employeeRepository = employeeRepository;
        //}

        //public DepartmentsController(EmployeeRepository employeeRepository)
        //{
        //    this.employeeRepository = employeeRepository;
        //}

        [HttpPost]
        public ActionResult Insert(Department department)
        {
            var hasil = departmentRepository.Insert(department);
            if (hasil != 0)
            {
                var get = departmentRepository.Get();
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Department Berhasil Di Masukan", Data = hasil });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Data Department Gagal Di Masukan", Data = hasil });
            }
            //------
            //if (employeeRepository.IsDepIdExists(department.ID))
            //{
            //    return StatusCode(409, new object { status = HttpStatusCode.Conflict, message = "Data Already Exists in Database", Data = department });

            //}
            //employeeRepository.Insert(department);
            //return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Ditambahkan", Data = department})

        }

        // method get() sebelum async
        //[HttpGet]
        //public ActionResult Get()
        //{
        //    var get = departmentRepository.Get();
        //    if (get.Count() != 0)
        //    {
        //        return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Department Ditemukan", Data = get });
        //    }
        //    else
        //    {
        //        return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Department Tidak Ditemukan", Data = get });
        //    }
        //}

        // method get() setelah dijadikan async 19/05/2023
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var get = await departmentRepository.Get();
            if (get.Count() != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Department Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Department Tidak Ditemukan", Data = get });
            }
        }

        [HttpGet("{Id}")]
        public ActionResult Get(int Id)
        {
            var get = departmentRepository.Get(Id);

            if (get == null)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Ditemukan", Data = get });
            }
        }

        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            var hapus = departmentRepository.Get(Id);
            if (hapus == null)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan.", Data = hapus });
            }
            var delete = departmentRepository.Delete(Id);
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Di Hapus" });
        }

        [HttpPut]
        public ActionResult Update(Department department)
        {
            //var get = departmentRepository.Get(Id);
            //departmentRepository.Update(department);
            //return Ok();
            var get = departmentRepository.Update(department);
            if (get != 0)
            {
                return Ok();
            }

            return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Data Department Gagal Di Update", Data = get });

        }

    }

}
