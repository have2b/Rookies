using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class TaskDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
    }
}