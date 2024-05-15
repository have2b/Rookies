using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Domain.Entities
{
    [Table("ProjectEmployees")]
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
