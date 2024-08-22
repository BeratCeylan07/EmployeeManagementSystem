using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.DataAccess.Abstract;

public interface IDepartmentRepository : IBaseRepository<Department>
{
    Task<Department> GetDepartmentWithEmployees(int departmentId);
    Task<List<Department>> GetAllWithEmployeeCount();
    Task<List<Department>> GetAllWithSalaryTotal();
    
    Task<bool> HasEmployees(int departmentId);

}