using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("project_employees")]
    public class ProjectEmployee
    {
        [ForeignKey("ProjectId")]
        public Guid ProjectId { get; set; }

        [ForeignKey("EmployeeId")]
        public Guid EmployeeId { get; set; }
        public bool Enable { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Project? Project { get; set; }
    }
}
