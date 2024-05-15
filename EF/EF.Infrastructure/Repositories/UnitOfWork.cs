using EF.Domain.Entities;
using EF.Domain.Interfaces;
using EF.Infrastructure.Data;

namespace EF.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Departments = new Repository<Department>(_dbContext);
            Projects = new Repository<Project>(_dbContext);
            Employees = new Repository<Employee>(_dbContext);
            ProjectEmployees = new Repository<ProjectEmployee>(_dbContext);
            Salaries = new Repository<Salary>(_dbContext);
        }

        public IRepository<Department> Departments { get; private set; }
        public IRepository<Project> Projects { get; private set; }
        public IRepository<Employee> Employees { get; private set; }
        public IRepository<ProjectEmployee> ProjectEmployees { get; private set; }
        public IRepository<Salary> Salaries { get; private set; }

        public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
