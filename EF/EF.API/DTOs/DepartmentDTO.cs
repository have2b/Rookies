using System.ComponentModel.DataAnnotations;

namespace EF.API.DTOs
{
    public record DepartmentDTO
    {
        [Required]
        public string? Name { get; set; }
    }
}
