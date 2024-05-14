using EFCore.Models;
using EFCore.Repositories;
using EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddAutoMapper(typeof(Program));
    // Configure services 
    builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    {
        opts.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
    });

    builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
    builder.Services.AddScoped<IProjectEmployeeRepository, ProjectEmployeeRepository>();
    builder.Services.AddScoped<ISalaryRepository, ISalaryRepository>();
}

var app = builder.Build();
{
    // Configure request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
app.MapControllers();
app.Run();