using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Domains;
using Sample.Services;
using System;

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("Save")]
        [Authorize] // Assume you have authentication set up, adjust as needed
        public IActionResult Save([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid employee data");
            }
            try
            {
                _employeeService.Save(employee);
                return Ok("Employee saved successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving employee: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        [Authorize] // Assume you have authentication set up, adjust as needed
        public IActionResult GetAll([FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string gender)
        {
            // Perform additional input validation if necessary

            var employees = _employeeService.GetAll(firstName, lastName, gender);
            return Ok(employees);
        }
    }
}


