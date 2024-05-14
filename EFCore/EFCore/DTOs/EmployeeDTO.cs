using System.ComponentModel.DataAnnotations;

namespace EFCore.DTOs
{
    public record EmployeeDTO
    {
        [Required]
        public string? Name { get; set; }
    }
}