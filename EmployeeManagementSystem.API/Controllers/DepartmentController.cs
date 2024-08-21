using EmployeeManagementSystem.Business.Abstract;
using EmployeeManagementSystem.Business.Concrete.Dtos.Departments;
using EmployeeManagementSystem.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    [Route("api/department/[action]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {   
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<ActionResult<DepartmentDetailDto>> GetDepartmentWithEmployees(int id)
        {
            var departmentDetail = await _departmentService.GetDepartmentWithEmployees(id);

            if (departmentDetail == null)
            {
                return NotFound();
            }

            return Ok(departmentDetail);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDepartmentsWithEmployeeCount()
        {
            var departments = await _departmentService.GetAllDepartmentsWithEmployeeCount();
            return Ok(departments);
        }
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var departments = _departmentService.GetAllDepartments();
            return Ok(departments);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] AddDepartmentDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            try
            {
                bool result = await _departmentService.Add(request);
                if (result)
                {
                    return Ok("Department created successfully");
                }
                else
                {
                    return BadRequest("Failed to create department");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the department");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
        {
            if (id != updateDepartmentDto.ID)
            {
                return BadRequest("ID in the URL does not match the ID in the request body.");
            }
            

            bool result = await _departmentService.Update(updateDepartmentDto);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound($"Department with ID {id} not found.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                bool result = await _departmentService.Delete(id);

                if (result)
                {
                    return NoContent(); // 204 No Content
                }
                else
                {
                    return NotFound($"Department with ID {id} not found.");
                }
            }
            catch (InvalidOperationException ex) when (ex.Message == "Cannot delete department with existing employees.")
            {
                return BadRequest("Cannot delete the department because it has existing employees. Please remove or reassign the employees first.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }
    }
}
