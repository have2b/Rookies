using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Models
{
    [Table("salaries")]
    public class Salary
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("EmployeeId")]
        public Guid EmployeeId { get; set; }

        [Required, Precision(4, 2)]
        public decimal Amount { get; set; }

        [JsonIgnore]
        public virtual Employee? Employee { get; set; }
    }
}
