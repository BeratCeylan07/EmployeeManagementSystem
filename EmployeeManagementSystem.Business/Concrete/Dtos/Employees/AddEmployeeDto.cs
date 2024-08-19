namespace EmployeeManagementSystem.Business.Concrete.Dtos.Employees;

public class AddEmployeeDto
{
    public int DepartmentId { get; set; }
    public int userID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public double Salary { get; set; }
    public string Password { get; set; } = string.Empty;
}