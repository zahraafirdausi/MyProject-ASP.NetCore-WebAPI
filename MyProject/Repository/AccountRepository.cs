using BCrypt.Net;
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
    public class AccountRepository : IAccountRepository
    {
        private readonly MyContext myContext;
        private int NIK;
        private int Email;

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<Account> Get()
        {
            return myContext.Accounts.ToList();
        }

        public Account Get(string NIK)
        {
            return myContext.Accounts.Find(NIK);
        }

        //login
        public IEnumerable<Object> GetLogin(string Email, string Password)
        {
            //Cara 1
            //var check = myContext.Accounts.Where(a => a.Employee.Email == Email &&  a.Password == Password).FirstOrDefault();
            //Cara 2
            //tambah bcryp untuk mengenkripsi
            var account = myContext.Accounts.FirstOrDefault(a => a.Employee.Email == Email);
            if (account != null)
            {
                var myPass = BCrypt.Net.BCrypt.Verify(Password, account.Password);
                var myobject = (from a in myContext.Accounts
                                join e in myContext.Employees
                                on a.NIK equals e.NIK
                                where Email == e.Email && myPass == true
                                select new
                                {
                                    NIK = a.NIK,
                                    Password1 = a.Password,
                                    Employee = a.Employee
                                });




                if (myobject.Count() != 0)
                {
                    return myobject.ToList();
                }
                else
                {

                    return null;
                }
            }
            return null;
        }

        //LOGIN WITH VM
        public Object GetLoginVM(LoginVM loginVM)
        {
            var account = myContext.Accounts.FirstOrDefault(a => a.Employee.Email == loginVM.Email);
            if (account != null)
            {
                var myPass = BCrypt.Net.BCrypt.Verify(loginVM.Password, account.Password);
                var myobject = (from a in myContext.Accounts
                                join e in myContext.Employees
                                on a.NIK equals e.NIK
                                where loginVM.Email == e.Email && myPass == true
                                select new
                                {
                                    NIK = a.NIK,
                                    Password = a.Password,
                                    Employee = a.Employee
                                });




                if (myobject.Count() != 0)
                {
                    return myobject.ToList();
                }
                else
                {

                    return null;
                }
            }
            return null;
        }


        //fungsi generate NIK
        public string GetNIK()
        {
            var lastEmployee = myContext.Employees.OrderByDescending(e => e.NIK).FirstOrDefault();
            var currentDate = DateTime.Now.ToString("ddMMyyyy");
            int sequenceNumber = 1;
            if (lastEmployee != null && lastEmployee.NIK.StartsWith(currentDate))
            {
                sequenceNumber = int.Parse(lastEmployee.NIK.Substring(currentDate.Length)) + 1;
            }
            string newNIK = $"{currentDate}{sequenceNumber.ToString("D3")}";
            return newNIK;
        }

        // register pake vm
        public int Register(RegisterVM registerVM)
        {

            Employee employee = new Employee
            {
                NIK = GetNIK(),
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = (Gender)registerVM.Gender,
                DepartmentId = registerVM.DepartmentId
            };

            var checkEmail = myContext.Employees.Where(e => e.Email == employee.Email).FirstOrDefault();
            var checkPhone = myContext.Employees.Where(e => e.Phone == employee.Phone).FirstOrDefault();
            var checkDeptId = myContext.Departments.Find(registerVM.DepartmentId);
            if (checkEmail == null && checkPhone == null)
            {
                if(checkDeptId != null)
                {
                    var enkripsi = BCrypt.Net.BCrypt.HashPassword(registerVM.Password);

                    myContext.Employees.Add(employee);
                    Account account = new Account
                    {
                        NIK = employee.NIK,
                        Password = enkripsi //registerVM.Password 
                    };

                    myContext.Accounts.Add(account);

                    var save = myContext.SaveChanges();
                    return save;
                }
                return 11;
            }
            return 0;
        }

        public int Update(Account account)
        {
            //taroh enkripsi
            var enkripsi = BCrypt.Net.BCrypt.HashPassword(account.Password);
            account.Password = enkripsi;
            myContext.Entry(account).State = EntityState.Modified;
            var saved = myContext.SaveChanges();
            return saved;

        }
        public int Delete(string NIK)
        {
            var acc = myContext.Accounts.Find(NIK);
            myContext.Accounts.Remove(acc);
            var saved = myContext.SaveChanges();
            return saved;
        }


    }
}











//Register
//public int Insert(Account account)
//{
//    var checkAccNIK = myContext.Accounts.Where(a => a.NIK == account.NIK).FirstOrDefault();
//    if (checkAccNIK == null)
//    {
//        myContext.Entry(account).State = EntityState.Added;
//        var save = myContext.SaveChanges();
//        return save;
//    }
//    else
//    {
//        return 0;
//    }
//}