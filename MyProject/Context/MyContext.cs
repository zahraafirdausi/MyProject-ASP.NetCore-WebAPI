using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using MyProject.Model;

namespace MyProject.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Account> Accounts { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employee>()
        //        .HasOne(e => e.Account)
        //        .WithOne(a => a.Employee)
        //        .HasForeignKey<Account>(a => a.NIK);
        //}
    }
}
