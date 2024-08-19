using AutoMapper;
using EmployeeManagementSystem.Business.Concrete.Dtos.Departments;
using EmployeeManagementSystem.Business.Concrete.Dtos.Employees;
using EmployeeManagementSystem.Business.Core;
using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.Business.Concrete.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<AddDepartmentDto, Department>()
            .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.ISCREATEDDATE, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ISMODIFIEDDATE, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ISCREATEDUSERID, opt => opt.MapFrom(src => src.userID))
            .ForMember(dest => dest.ISMODIFIEDUSERID, opt => opt.MapFrom(src => src.userID));
        
        CreateMap<AddEmployeeDto, Employee>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom<PasswordValueResolver>())
            .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.ISCREATEDDATE, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ISMODIFIEDDATE, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ISCREATEDUSERID, opt => opt.MapFrom(src => src.userID))
            .ForMember(dest => dest.ISMODIFIEDUSERID, opt => opt.MapFrom(src => src.userID));
        CreateMap<Employee, EmployeeWithDepartmentDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
        // Department -> DepartmentDetailDto mapping'i
        CreateMap<Department, DepartmentDetailDto>();

        // Employee -> EmployeeDto mapping'i
        CreateMap<Employee, EmployeeDto>();

        // Department -> DepartmentDetailDto mapping'i (Employees özelliği için özel mapping)
        CreateMap<Department, DepartmentDetailDto>()
            .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees));
        CreateMap<Department, DepartmentWithEmployeeCountDto>()
            .ForMember(dest => dest.EmployeeCount, opt => opt.MapFrom(src => src.Employees.Count));
        CreateMap<Employee, EmployeeDetailDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.departmenId, opt => opt.MapFrom(src => src.Department.ID))
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.EmployeePayments));

        CreateMap<EmployeePayment, EmployeePaymentDto>();
        
        CreateMap<UpdateDepartmentDto, Department>();
        CreateMap<UpdateEmployeeDto, Employee>();

    }
}