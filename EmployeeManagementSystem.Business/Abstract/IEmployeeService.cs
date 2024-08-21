using EmployeeManagementSystem.Business.Concrete.Dtos.Employees;
using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.Business.Abstract;

public interface IEmployeeService
{
    Task<bool> AddAsync(AddEmployeeDto employee);
    Task<bool> UpdateAsync(UpdateEmployeeDto updateEmployee);
    Task<bool> DeleteAsync(int employeeId);
    Task<List<EmployeeWithDepartmentDto>> GetAllEmployeesAsync();
    Task<EmployeeDetailDto> GetEmployeeDetailsAsync(int employeeId);
    Task<LoginResultDto> LoginAsync(LoginRequestDto loginRequest);
}