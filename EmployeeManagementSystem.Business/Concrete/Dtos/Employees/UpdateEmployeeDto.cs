namespace EmployeeManagementSystem.Business.Concrete.Dtos.Employees;

public class UpdateEmployeeDto
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public int userID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public double Salary { get; set; }
}