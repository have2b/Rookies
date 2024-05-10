using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public virtual Employee? Employee { get; set; }
    }
}
