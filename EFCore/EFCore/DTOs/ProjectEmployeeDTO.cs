using System.ComponentModel.DataAnnotations;

namespace EFCore.DTOs
{
    public record ProjectEmployeeDTO
    {
        public Guid EmployeeId { get; set; }
        [Required]
        public bool Enable { get; set; }
    }
}
