using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyProject.Model
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }

        //Untuk bikin foreign key bisa memakai 2 cara
        //Cara 1
        public int DepartmentId { get; set; }
        [JsonIgnore]
        public virtual Department? Department { get; set; }
        public virtual Account? Account { get; set; }
        //Cara 2
        //[ForeignKey("Department")]
        //public int Department_Id { get; set; }

        //public virtual Account Account { get; set; }
    }   public enum Gender
        {
            Male,
            Female
        }
    }
