using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.Business.Concrete.Dtos.Employees;

public class EmployeeDetailDto
{
    public int Id { get; set; }
    public int departmenId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public double Salary { get; set; }
    public string DepartmentName { get; set; }
    public List<EmployeePaymentDto> Payments { get; set; }
}

public class EmployeePaymentDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}