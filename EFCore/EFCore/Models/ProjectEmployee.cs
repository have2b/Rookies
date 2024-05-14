using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EFCore.Models
{
    [Table("ProjectEmployees")]
    public class ProjectEmployee
    {
        [ForeignKey("ProjectId")]
        public Guid ProjectId { get; set; }

        [ForeignKey("EmployeeId")]
        public Guid EmployeeId { get; set; }
        public bool Enable { get; set; }

        [JsonIgnore]
        public virtual Employee? Employee { get; set; }
        [JsonIgnore]
        public virtual Project? Project { get; set; }
    }
}
