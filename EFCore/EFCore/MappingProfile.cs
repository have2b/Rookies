using AutoMapper;
using EFCore.DTOs;
using EFCore.Models;

namespace EFCore
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDTO>();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Salary, SalaryDTO>();
            CreateMap<Salary, SalaryDTO>().ReverseMap();
        }
    }
}
