using EmployeeManagementSystem.Business.Concrete.Dtos.Employees;
using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.Business.Abstract;

public interface IEmployeeService
{
    bool Add(AddEmployeeDto employee);
    bool Update(UpdateEmployeeDto updateEmployee);
    bool Delete(int employeeId);
    List<EmployeeWithDepartmentDto> GetAllEmployees();
    EmployeeDetailDto GetEmployeeDetails(int employeeId);
    LoginResultDto Login(LoginRequestDto loginRequest);
}