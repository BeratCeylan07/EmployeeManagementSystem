namespace EmployeeManagementSystem.Business.Concrete.Dtos.Departments;

public class DepartmentDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<EmployeeDto> Employees { get; set; }
}

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}