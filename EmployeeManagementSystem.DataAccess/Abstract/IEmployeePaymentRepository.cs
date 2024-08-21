using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.DataAccess.Abstract;

public interface IEmployeePaymentRepository : IBaseRepository<EmployeePayment>
{
    Task<bool> HasEmployeesAsync(int emplooyePaymentId);
    Task<List<EmployeePayment>> GetAllPaymentsAsync();


}