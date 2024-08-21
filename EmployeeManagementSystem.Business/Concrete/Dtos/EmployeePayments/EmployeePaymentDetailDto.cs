namespace EmployeeManagementSystem.Business.Concrete.Dtos.EmployeePayments;

public class EmployeePaymentDetailDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string DepartmentName { get; set; }
}