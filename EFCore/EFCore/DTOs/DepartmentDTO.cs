using System.ComponentModel.DataAnnotations;

namespace EFCore.DTOs
{
    public record DepartmentDTO
    {
        [Required]
        public string? Name { get; set; }
    }
}
