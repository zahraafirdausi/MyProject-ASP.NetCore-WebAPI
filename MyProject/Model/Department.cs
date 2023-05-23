using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Model
{
    [Table("Department")]
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
