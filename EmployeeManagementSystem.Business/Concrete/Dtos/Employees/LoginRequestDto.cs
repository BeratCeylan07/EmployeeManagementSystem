namespace EmployeeManagementSystem.Business.Concrete.Dtos.Employees;

public class LoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}