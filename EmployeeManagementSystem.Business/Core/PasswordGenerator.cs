namespace EmployeeManagementSystem.Business.Core;

public static class PasswordGenerator
{
    private static readonly Random random = new Random();

    public static string GenerateNumericPassword(int length = 6)
    {
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}