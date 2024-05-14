using System.ComponentModel.DataAnnotations;

namespace EFCore.DTOs
{
    public record ProjectDTO
    {
        [Required]
        public string? Name { get; set; }
    }
}
