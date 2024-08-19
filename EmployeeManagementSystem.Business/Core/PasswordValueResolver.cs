using AutoMapper;
using EmployeeManagementSystem.Business.Concrete.Dtos.Employees;
using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.Business.Core;

public class PasswordValueResolver : IValueResolver<AddEmployeeDto, Employee, string>
{
    public string Resolve(AddEmployeeDto source, Employee destination, string destMember, ResolutionContext context)
    {
        return PasswordGenerator.GenerateNumericPassword();
    }
}