using EmployeeManagementSystem.Business.Concrete.Dtos.Departments;
using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.Business.Abstract;

public interface IDepartmentService
{
    bool Add(AddDepartmentDto addDepartmentDto);
    bool Update(UpdateDepartmentDto updateDepartmentDto);
    bool Delete(int departmentId);

    List<Department> GetAllDepartments();
    List<DepartmentWithEmployeeCountDto> GetAllDepartmentsWithEmployeeCount();

    DepartmentDetailDto GetDepartmentWithEmployees(int departmentId);

}