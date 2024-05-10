using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [ForeignKey("DepartmentId")]
        public Guid DepartmentId { get; set; }
        public DateTime JoinedDate { get; set; } = DateTime.Now;

        public virtual Salary? Salary { get; set; }
        public virtual Department? Department { get; set; }
        public virtual ICollection<ProjectEmployee>? ProjectEmployees { get; set; }
    }
}
