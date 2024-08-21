using EmployeeManagementSystem.Business.Abstract;
using EmployeeManagementSystem.Business.Concrete.Dtos.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    [Route("api/employee/[action]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeController(IEmployeeService employeeService, IHttpContextAccessor httpContextAccessor)
        {   
            _employeeService = employeeService;
            httpContextAccessor = _httpContextAccessor;
        }
        
        [HttpPost]
        public async Task<IActionResult> NewEmployee([FromBody] AddEmployeeDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            try
            {
                bool result = await _employeeService.AddAsync(request);
                if (result)
                {
                    return Ok("Employee Added successfully");
                }
                else
                {
                    return BadRequest("Failed to Added Employee");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the Employee");
            }
        }
        [HttpGet]
        public async Task<ActionResult<EmployeeDetailDto>> GetEmployeeDetails(int id)
        {
            var employeeDetails = await _employeeService.GetEmployeeDetailsAsync(id);
            if (employeeDetails == null)
            {
                return NotFound();
            }

            return Ok(employeeDetails);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> updateEmployee(int id, [FromBody] UpdateEmployeeDto updateemployeeDto)
        {
            if (id != updateemployeeDto.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the request body.");
            }

          

            bool result = await _employeeService.UpdateAsync(updateemployeeDto);

            if (result)
            {
                return NoContent(); // 204 No Content
            }
            else
            {
                return NotFound($"Department with ID {id} not found.");
            }
        }

        // DELETE endpoint'i
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                bool result = await _employeeService.DeleteAsync(id);

                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound($"Employee with ID {id} not found.");
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
