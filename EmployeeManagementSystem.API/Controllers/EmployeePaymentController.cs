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
        public async Task<IActionResult> NewEmployeePayment([FromBody] CreateEmployeePaymentDto request)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeePayment(int id)
        {
            try
            {
                bool result = await _employeePaymentService.DeleteAsync(id);

                if (result)
                {
                    return Ok("Employee Payment Is Removed successfully");
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
        [HttpPut("{id}")]
        public async Task<IActionResult> paymentEmployeeUpdate(int id, [FromBody] UpdateEmployeePaymentDto updateEmployeePaymentDto)
        {
            if (id != updateEmployeePaymentDto.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the request body.");
            }
            
            bool result = await _employeePaymentService.UpdateAsync(updateEmployeePaymentDto);

            if (result)
            {
                return Ok("Employee Payment is All Info Updated");
            }
            else
            {
                return NotFound($"Department with ID {id} not found.");
            }
        }
    }
}
