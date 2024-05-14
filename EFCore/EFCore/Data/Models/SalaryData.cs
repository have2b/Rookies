namespace EFCore.Data.Models
{
    public record SalaryData
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
    }
}
