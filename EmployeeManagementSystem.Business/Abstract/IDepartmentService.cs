using EmployeeManagementSystem.Business.Concrete.Dtos.Departments;
using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.Business.Abstract;

public interface IDepartmentService
{
    Task<bool> Add(AddDepartmentDto addDepartmentDto);
    Task<bool> Update(UpdateDepartmentDto updateDepartmentDto);
    Task<bool> Delete(int departmentId);

    Task<IEnumerable<Department>> GetAllDepartments();
    Task<IEnumerable<DepartmentWithEmployeeCountDto>> GetAllDepartmentsWithEmployeeCount();

    Task<DepartmentDetailDto> GetDepartmentWithEmployees(int departmentId);

}