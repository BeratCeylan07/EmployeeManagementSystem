namespace EmployeeManagementSystem.WebApp.Models;

public class LoginResponse
{
    public bool success { get; set; }
    public string? message { get; set; }
    public int employeeId { get; set; }
    public string? name { get; set; }
    public string? surname { get; set; }
    public string? email { get; set; }
    public string? token { get; set; }
}