using AutoMapper;
using EmployeeManagementSystem.Business.Concrete.Dtos.Departments;
using EmployeeManagementSystem.Business.Concrete.Dtos.EmployeePayments;
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
       
        CreateMap<Department, DepartmentDetailDto>();

        CreateMap<Employee, EmployeeDto>();
        
        CreateMap<Department, DepartmentDetailDto>()
            .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees));
        
        CreateMap<Department, DepartmentWithEmployeeCountDto>()
            .ForMember(dest => dest.EmployeeCount, opt => opt.MapFrom(src => src.Employees.Count));
        
        CreateMap<Employee, EmployeeDetailDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.departmenId, opt => opt.MapFrom(src => src.Department.ID))
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.EmployeePayments));
        
        CreateMap<UpdateDepartmentDto, Department>();
        
        CreateMap<UpdateEmployeeDto, Employee>();
        
        CreateMap<EmployeePayment, EmployeePaymentDto>()
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.ISCREATEDDATE))
            .ForMember(dest => dest.EmployeeDepartment, opt => opt.MapFrom(src => src.Employee.Department.Name))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name + " " + src.Employee.Surname));
            
        CreateMap<CreateEmployeePaymentDto, EmployeePayment>()
            .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.ISCREATEDDATE, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ISMODIFIEDDATE, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ISCREATEDUSERID, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ISMODIFIEDUSERID, opt => opt.MapFrom(src => src.UserId));

        CreateMap<UpdateEmployeePaymentDto, EmployeePayment>()
            .ForMember(dest => dest.ISMODIFIEDDATE, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ISMODIFIEDUSERID, opt => opt.MapFrom(src => src.UserId));

        CreateMap<EmployeePayment, EmployeePaymentDetailDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Employee.Department.Name));
    }
    
}