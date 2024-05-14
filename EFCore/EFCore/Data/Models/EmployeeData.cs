namespace EFCore.Data.Models
{
    public record EmployeeData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
