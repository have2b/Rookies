using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models;

[Table("Departments")]
public class Department
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public virtual ICollection<Employee>? Employees { get; set; }
}
