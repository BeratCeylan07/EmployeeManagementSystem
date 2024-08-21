using EmployeeManagementSystem.Business.Abstract;
using EmployeeManagementSystem.Business.Concrete.Dtos.EmployeePayments;
using EmployeeManagementSystem.DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    [Route("api/emplooyePayment/[action]")]
    [ApiController]
    [Authorize]
    public class EmployeePaymentController : ControllerBase
    {
        private readonly IEmployeePaymentService _employeePaymentService;

        public EmployeePaymentController(IEmployeePaymentService employeePaymentService)
        {   
            _employeePaymentService = employeePaymentService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeePayments()
        {
            var employeePayments = await _employeePaymentService.GetAllPaymentsAsync();
            return Ok(employeePayments);
        }
        [HttpPost]
        public async Task<IActionResult> NewEmployee([FromBody] CreateEmployeePaymentDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            try
            {
                bool result = await _employeePaymentService.AddAsync(request);
                if (result)
                {
                    return Ok("Employee's Of Payment is Adding successfully");
                }
                else
                {
                    return BadRequest("Failed to Added Employee Payment");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the Employee");
            }
        }
    }
}
