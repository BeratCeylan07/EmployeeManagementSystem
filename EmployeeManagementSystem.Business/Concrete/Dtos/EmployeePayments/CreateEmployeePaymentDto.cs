namespace EmployeeManagementSystem.Business.Concrete.Dtos.EmployeePayments;

public class CreateEmployeePaymentDto
{
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int EmployeeId { get; set; }
    public int UserId { get; set; }
}
