using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Domain.Entities
{
    [Table("Salaries")]
    public class Salary
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("EmployeeId")]
        public Guid EmployeeId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
