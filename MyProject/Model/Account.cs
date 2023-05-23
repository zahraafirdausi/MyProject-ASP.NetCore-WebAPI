using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyProject.Model
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [ForeignKey("Employee")]
        public string NIK { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public virtual Employee? Employee { get; set; }

    }
}
