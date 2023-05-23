using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Model;
using MyProject.Repository.Interface;
using System.Collections;
using System.Net;

namespace MyProject.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyContext myContext;

        public DepartmentRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        // Method awal, belum menggunakan async
        //public IEnumerable<Department> Get()
        //{
        //    return myContext.Departments.ToList();
        //}

        // We will transform this method Get() into async 19/05/2023
        // 1. add Async Keyword
        // 2. add Task<> Keyword
        // 3. add Await
        // 4. Ganti ToList() dengan ToListAsync()
        public async Task<IEnumerable<Department>> Get()
        {
            return await myContext.Departments.ToListAsync();
        }



        public Department Get(int Id)
        {
            return myContext.Departments.Find(Id);
        }

        public int Insert(Department department)
        {
            //mencari department di database
            var checkDepartment = myContext.Departments.Where(d => d.Name == department.Name).FirstOrDefault();
            if (checkDepartment != null)
            {
                return 0; // Jika nama department sudah ada, return 0
            }
            else
            {
                // mengambil nilai dari Id terakhir
                var lastDepartment = myContext.Departments.OrderByDescending(d => d.Id).FirstOrDefault();
                int newID = 1; // jika belum ada maka akan Id = 1
                if (lastDepartment != null) // jika id tidak sama sama null
                {
                    newID = lastDepartment.Id + 1; // Membuat auto increment ID
                }
                department.Id = newID;
                myContext.Departments.Add(department); //Menambahkan dengan method Add
                var save = myContext.SaveChanges(); // Menyimpan Perubahan
                return save;
            }
        }

        public int Update(Department department)
        {
            //mencari department di database
            var checkDept = myContext.Departments.Where(d => d.Name == department.Name).FirstOrDefault();
            if (checkDept != null)
            {
                return 0; // Jika nama department sudah ada, return 0
            }
            myContext.Departments.Update(department);
            var saved = myContext.SaveChanges();
            return saved;

        }
        public int Delete(int Id)
        {
            var departement = myContext.Departments.Find(Id);
            myContext.Departments.Remove(departement);
            var saved = myContext.SaveChanges();
            return saved;
        }
    }
}








////
///insert
//myContext.Departments.Add(department);
//var save = myContext.SaveChanges();
//return save;
//--------------

//------------------
//myContext.Entry<department>.State = EntityState.Detached;
//myContext.Departments.Update(department);
//var saved = myContext.SaveChanges();
//return saved;
//-------------------