namespace EmployeeManagementSystem.Business.Concrete.Dtos.Employees;

public class EmployeeWithDepartmentDto
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public double Salary { get; set; }
    public string Password { get; set; }
    public string DepartmentName { get; set; }
}