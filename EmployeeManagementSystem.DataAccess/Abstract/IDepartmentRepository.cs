using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.DataAccess.Abstract;

public interface IDepartmentRepository : IBaseRepository<Department>
{
    Department GetDepartmentWithEmployees(int departmentId);
    List<Department> GetAllWithEmployeeCount();
    
    bool HasEmployees(int departmentId);

}