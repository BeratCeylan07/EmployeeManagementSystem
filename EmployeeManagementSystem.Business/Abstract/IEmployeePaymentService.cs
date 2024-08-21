using EmployeeManagementSystem.Business.Concrete.Dtos.EmployeePayments;
using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.Business.Abstract;

public interface IEmployeePaymentService
{
    Task<bool> AddAsync(CreateEmployeePaymentDto addDepartmentDto);
    Task<bool> UpdateAsync(UpdateEmployeePaymentDto updateDepartmentDto);
    Task<bool> DeleteAsync(int departmentId);

    Task<List<EmployeePaymentDto>> GetAllPaymentsAsync();
    
    Task<EmployeePaymentDetailDto> GetEmplooyeePaymentInfoAsync(int departmentId);
    

}