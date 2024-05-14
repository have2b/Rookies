using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EFCore.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [ForeignKey("DepartmentId")]
        public Guid DepartmentId { get; set; }
        public DateTime JoinedDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public virtual Salary? Salary { get; set; }
        [JsonIgnore]
        public virtual Department? Department { get; set; }
        public virtual ICollection<ProjectEmployee>? ProjectEmployees { get; set; }
    }
}
