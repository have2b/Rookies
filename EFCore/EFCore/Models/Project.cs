using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public virtual ICollection<ProjectEmployee>? ProjectEmployees { get; set; }
    }
}
