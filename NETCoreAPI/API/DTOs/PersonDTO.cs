using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.DTOs
{
    public class PersonDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string BirthPlace { get; set; }
    }
}
