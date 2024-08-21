using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.DataAccess.Abstract;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    Task<Employee> GetEmployeeWithDetailsAsync(int employeeId);
    Task<Employee> GetByEmailAndPasswordAsync(string email, string password);
    Task<bool> HasEmployeesAsync(int employeeId);
    Task<List<Employee>> GetAllEmployeesAsync();

}