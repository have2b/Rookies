using EF.Domain.Entities;

namespace EF.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Department> Departments { get; }
        IRepository<Project> Projects { get; }
        IRepository<Employee> Employees { get; }
        IRepository<ProjectEmployee> ProjectEmployees { get; }
        IRepository<Salary> Salaries { get; }

        Task<int> SaveAsync();
    }
}
