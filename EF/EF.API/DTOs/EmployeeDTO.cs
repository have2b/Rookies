using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.API.DTOs
{
    public record EmployeeDTO
    {
        [Required]
        public string? Name { get; set; }
        public Guid DepartmentId { get; set; }
        public decimal Amount { get; set; }
    }
}
