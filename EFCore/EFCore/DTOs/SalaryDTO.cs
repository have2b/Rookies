using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EFCore.DTOs
{
    public record SalaryDTO
    {
        [Required, Precision(4, 2)]
        public decimal Amount { get; set; }
    }
}
