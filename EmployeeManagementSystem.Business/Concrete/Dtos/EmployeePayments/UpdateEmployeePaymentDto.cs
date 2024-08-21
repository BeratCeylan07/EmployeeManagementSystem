namespace EmployeeManagementSystem.Business.Concrete.Dtos.EmployeePayments;

public class UpdateEmployeePaymentDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int EmployeeId { get; set; }
    public int UserId { get; set; }
}