using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyProject.Context;
using MyProject.Model;
using MyProject.Repository.Interface;
using MyProject.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;


namespace MyProject.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext myContext;
        //private int Id;

        public EmployeeRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(string NIK)
        {
            var employee = myContext.Employees.Find(NIK);
            myContext.Employees.Remove(employee);
            var saved = myContext.SaveChanges();
            return saved;
        }
        //public IEnumerable<Employee> Get()
        //{
        //    return myContext.Employees.ToList();
        //}

        public Employee Get(string NIK)
        {
            //ada 3 cara
            return myContext.Employees.Find(NIK);
            //return myContext.Employees.Where(e => e.NIK == NIK).FirstOrDefault();
            //return myContext.Employees.Where(e => e.NIK == NIK).SingleOrDefault();
            //return myContext.Employees.Include(e => e.Department).FirstOrDefault(e => e.NIK == NIK);
        }

        public IEnumerable<Department> Get_Department()
        {
            return myContext.Departments.ToList();
        }

        public Department Get_DepartmentID(int Id)
        {
            return myContext.Departments.Find(Id);
        }
        
        //get NIK, FullName, Department Name
        public IEnumerable<Object> GetRows()
        {
            var myobject = (from e in myContext.Employees
                            join d in myContext.Departments
                            on e.Department.Id equals d.Id
                            select new
                            {
                                NIK = e.NIK,
                                FullName = e.FirstName + " " + e.LastName,
                                DepartmentName = d.Name
                            });
            if (myobject.Count() != 0)
            {
                return myobject.ToList();
            }
            return null;
        }


        // Cara pake lazy loading
        public IEnumerable<Employee> Get()
        {
            return myContext.Employees.ToList();
        }

        //fungsi generate NIK
        public string GetNIK()
        {
            //var lastEmployee = myContext.Employees.OrderByDescending(e => e.NIK).FirstOrDefault();
            //var currentDate = DateTime.Now.ToString("ddMMyyyy");
            //int sequenceNumber = 1;
            //if (lastEmployee != null && lastEmployee.NIK.StartsWith(currentDate))
            //{
            //    sequenceNumber = int.Parse(lastEmployee.NIK.Substring(currentDate.Length)) + 1;
            //}
            //string newNIK = $"{currentDate}{sequenceNumber.ToString("D3")}";
            //return newNIK;
            var currentDate = DateTime.Now.ToString("ddMMyyyy");
            int countEmployee = myContext.Employees.Count();
            if (countEmployee == 0)
            {
                string newNIK = DateTime.Now.ToString("ddMMyyyy") + "000";
                return newNIK;
            }
            return $"{currentDate}{countEmployee.ToString("D3")}";
        }

        //fungsi vm
        // register pake vm
        //public int Register(RegisterVM registerVM)
        //{
        //    Employee employee = new Employee
        //    {
        //        NIK = GetNIK(),
        //        FirstName = registerVM.FirstName,
        //        LastName = registerVM.LastName,
        //        Phone = registerVM.Phone,
        //        BirthDate = registerVM.BirthDate,
        //        Salary = registerVM.Salary,
        //        Email = registerVM.Email,
        //        Gender = (Gender)registerVM.Gender
        //    };
        //    myContext.Employees.Add(employee);

        //    Account account = new Account
        //    {
        //        NIK = employee.NIK,
        //        Password = registerVM.Password
        //    };
        //    myContext.Accounts.Add(account);

        //    Department department = new Department
        //    {
        //        Id = registerVM.Id
        //    };
        //    myContext.Departments.Add(department);
        //    myContext.SaveChanges();
        //}

        public int Insert(Employee employee)
    {
        bool isDuplicate = false;
        var checkEmail = myContext.Employees.Where(e => e.Email == employee.Email).FirstOrDefault();
        var checkPhone = myContext.Employees.Where(e => e.Phone == employee.Phone).FirstOrDefault();
        if (checkEmail != null || checkPhone != null)
        {
            isDuplicate = true; // Jika nomor HP atau email sudah terdaftar, set isDuplicate true
        }
        if (!isDuplicate)
        {
            employee.NIK = GetNIK();
            myContext.Entry(employee).State = EntityState.Added;
            var save = myContext.SaveChanges();
            return save;
        }
        else
        {
            return 0; // Jika isDuplicate true, return 0
        }
    }

    public int Update(Employee employee)
    {
        myContext.Employees.Update(employee);
        var saved = myContext.SaveChanges();
        return saved;
    }


}
}







//update
//var employee1 = myContext.Employees.Find(employee.NIK);
//-----------
//myContext.Employees.Update(employee);
//var saved = myContext.SaveChanges();
//return saved;
//--------
//find
//return myContext.Employees.Include(e => e.Department).Where(e => e.NIK == NIK).FirstOrDefault();
//return myContext.Departments.Include(d => d.Id).Where(e => e.Id == Id).FirstOrDefault();
//return myContext.Employees.Include(e => e.NIK).Where(d => d.Id == Id).FirstOrDefault();
//return myContext.Employees.Where(d => d.Id == Id).FirstOrDefault();
//----
//pake cara eagerload tanpa lazy loading - yg pake select namanya explisit
//public IEnumerable<Employee> Get()
//{
//    return myContext.Employees.Include(e => e.Department).Select(e => new Employee
//    {
//        NIK = e.NIK,
//        FirstName = e.FirstName,
//        LastName = e.LastName,
//        Phone = e.Phone,
//        BirthDate = e.BirthDate,
//        Salary = e.Salary,
//        Email = e.Email,
//        Gender = e.Gender.ToString(),
//        Department = e.Department
//    }).ToList();
//}
//-------
//Getrow
//return myContext.Employees.Include(e => e.Department).Select(e => new Employee
//{
//    NIK = e.NIK,
//    FullName = $"{e.FirstName} {e.LastName}",
//    Department = e.Department
//}).ToList();
//----
//insert
//--------
//---------------
//bool isDuplicate = false;
//var checkEmail = myContext.Employees.Where(e => e.Email == employee.Email).FirstOrDefault();
//var checkPhone = myContext.Employees.Where(e => e.Phone == employee.Phone).FirstOrDefault();

//if (checkEmail != null || checkPhone != null)
//{
//    isDuplicate = true;
//}
//else
//{
//    var lastEmployee = myContext.Employees.OrderByDescending(e => e.NIK).FirstOrDefault();
//    var currentDate = DateTime.Now.ToString("ddMMyyyy");
//    int sequenceNumber = 1;
//    if (lastEmployee != null && lastEmployee.NIK.StartsWith(currentDate))
//    {
//        sequenceNumber = int.Parse(lastEmployee.NIK.Substring(currentDate.Length)) + 1;
//    }
//    string newNIK = $"{currentDate}{sequenceNumber.ToString("D3")}";
//    employee.NIK = newNIK;
//    myContext.Employees.Add(employee);
//    var save = myContext.SaveChanges();
//    return save;
//}
//if (isDuplicate)
//{
//    return 0;
//}
//else
//{
//    return 1;
//}

//-----------------
//--------
//bool isDuplicate = false;
//var checkDuplicate = myContext.Employees.Where(e => e.Email == employee.Email || e.Phone == employee.Phone).FirstOrDefault();
//if (checkDuplicate != null)
//{
//    isDuplicate = true;
//}
//else
//{
//    var lastEmployee = myContext.Employees.OrderByDescending(e => e.NIK).FirstOrDefault();
//    var currentDate = DateTime.Now.ToString("ddMMyyyy");
//    int sequenceNumber = 1;
//    if (lastEmployee != null && lastEmployee.NIK.StartsWith(currentDate))
//    {
//        sequenceNumber = int.Parse(lastEmployee.NIK.Substring(currentDate.Length)) + 1;
//    }
//    string newNIK = $"{currentDate}{sequenceNumber.ToString("D3")}";
//    employee.NIK = newNIK;
//    myContext.Employees.Add(employee);
//    var save = myContext.SaveChanges();
//    return save;
//}

//if (isDuplicate)
//{
//    return 0;
//}
//else
//{
//    return 1;
//}
//---------
//var checkDuplicate = myContext.Employees.Where(e => e.Email == employee.Email || e.Phone == employee.Phone).FirstOrDefault();
// generate id
//var lastEmployee = myContext.Employees.OrderByDescending(e => e.NIK).FirstOrDefault();
//var currentDate = DateTime.Now.ToString("ddMMyyyy");
//int sequenceNumber = 1;
//if (lastEmployee != null && lastEmployee.NIK.StartsWith(currentDate))
//{
//    sequenceNumber = int.Parse(lastEmployee.NIK.Substring(currentDate.Length)) + 1;
//}
////D3 = pesan 3 tempat
//string newNIK = $"{currentDate}{sequenceNumber.ToString("D3")}";

//// set id of new employee
//employee.NIK = newNIK;

//// save the new employee
//myContext.Employees.Add(employee);
//var save = myContext.SaveChanges();
//return save;

//-----------
//insert pas ada department
//bool isDuplicate = false;
//var checkEmail = myContext.Employees.Where(e => e.Email == employee.Email).FirstOrDefault();
//var checkPhone = myContext.Employees.Where(e => e.Phone == employee.Phone).FirstOrDefault();
//if (checkEmail != null || checkPhone != null)
//{
//    isDuplicate = true; // Jika nomor HP atau email sudah terdaftar, set isDuplicate true
//}

//if (employee.Department != null)
//{
//    // Cek apakah department dengan ID yang dimasukkan sudah ada atau belum
//    var checkDepartment = myContext.Departments.Where(d => d.Id == employee.Department.Id).FirstOrDefault();
//    if (checkDepartment == null)
//    {
//        return 0; // Jika department tidak ditemukan, return 0
//    }
//}

//if (!isDuplicate)
//{
//    var lastEmployee = myContext.Employees.OrderByDescending(e => e.NIK).FirstOrDefault();
//    var currentDate = DateTime.Now.ToString("ddMMyyyy");
//    int sequenceNumber = 1;
//    if (lastEmployee != null && lastEmployee.NIK.StartsWith(currentDate))
//    {
//        sequenceNumber = int.Parse(lastEmployee.NIK.Substring(currentDate.Length)) + 1;
//    }
//    string newNIK = $"{currentDate}{sequenceNumber.ToString("D3")}";
//    employee.NIK = newNIK;
//    myContext.Employees.Add(employee);
//    var save = myContext.SaveChanges();
//    return save;
//}
//else
//{
//    return 0; // Jika isDuplicate true, return 0
//}
//-----------
//myContext.Employees.Add(employee);
//var save = myContext.SaveChanges();
//return save;

////----------
////Department
//public IEnumerable<Department> Get_Department()
//{
//    return myContext.Departments.ToList();
//}

//public Department Get_DepartmentID(int ID)
//{
//    return myContext.Departments.Find(ID);
//}

//public int Insert(Department department)
//{
//    myContext.Departments.Add(department);
//    var save = myContext.SaveChanges();
//    return save;
//}

//public int Update(Department department)
//{
//    myContext.Departments.Update(department);
//    var saved = myContext.SaveChanges();
//    return saved;
//}
//public int Delete(int ID)
//{
//    var departement = myContext.Departments.Find(ID);
//    myContext.Departments.Remove(departement);
//    var saved = myContext.SaveChanges();
//    return saved;
//}
////--------