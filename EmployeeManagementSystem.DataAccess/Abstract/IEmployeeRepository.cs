using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.DataAccess.Abstract;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    Employee GetEmployeeWithDetails(int employeeId);
    Employee GetByEmailAndPassword(string email, string password);

    bool HasEmployees(int employeeId);


}