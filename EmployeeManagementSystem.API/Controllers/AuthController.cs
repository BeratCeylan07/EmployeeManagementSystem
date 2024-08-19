using EmployeeManagementSystem.Business.Abstract;
using EmployeeManagementSystem.Business.Concrete.Dtos.Employees;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    [Route("api/auth/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public AuthController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _employeeService.Login(loginRequest);

            if (result.Success)
            {
                HttpContext.Session.SetInt32("USER_ID", result.EmployeeId);

                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
        }
    }
}
