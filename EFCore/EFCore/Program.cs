using EFCore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    // Configure services 
    builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    {
        opts.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
    });
}

var app = builder.Build();
{
    // Configure request pipeline
}

app.Run();