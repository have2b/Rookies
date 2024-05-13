using Microsoft.EntityFrameworkCore;

namespace EFCore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectEmployee>().HasKey(pe => new { pe.EmployeeId, pe.ProjectId });

            modelBuilder
                .Entity<Department>()
                .HasData(
                    new Department() { Id = Guid.NewGuid(), Name = "Software Development" },
                    new Department() { Id = Guid.NewGuid(), Name = "Finance" },
                    new Department() { Id = Guid.NewGuid(), Name = "Accountant" },
                    new Department() { Id = Guid.NewGuid(), Name = "HR" }
                );
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
    }
}
