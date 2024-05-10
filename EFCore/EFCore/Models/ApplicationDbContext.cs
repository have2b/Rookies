using Microsoft.EntityFrameworkCore;

namespace EFCore.Models
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            var config = builder.Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DB"));
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
