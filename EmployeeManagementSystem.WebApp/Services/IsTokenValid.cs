using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EmployeeManagementSystem.WebApp.Services
{
    public class TokenValidator
    {
        private readonly IConfiguration _configuration;

        public TokenValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsTokenValid(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
                var jwtToken = securityToken as JwtSecurityToken;

                return jwtToken != null && jwtToken.ValidTo >= DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                // Hata mesajını loglayın
                Console.WriteLine($"Token doğrulama hatası: {ex.Message}");
                return false;
            }
        }
    }
}