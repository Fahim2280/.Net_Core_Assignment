using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee updateDto)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            // Check for duplicate EmployeeCode
            var existingEmployeeWithCode = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeCode == updateDto.EmployeeCode && e.EmployeeId != id);

            if (existingEmployeeWithCode != null)
            {
                return BadRequest("Employee code already exists.");
            }

                employee.EmployeeName = updateDto.EmployeeName;
                employee.EmployeeCode = updateDto.EmployeeCode;
                employee.EmployeeSalary = updateDto.EmployeeSalary;
                employee.SupervisorId = updateDto.SupervisorId;


                _context.Entry(employee).State = EntityState.Modified;

           
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception
                return StatusCode(500, "An error occurred while saving the changes.");
            }

            return NoContent();
        }

        [HttpGet("thirdhighestsalary")]
        public async Task<ActionResult<Employee>> GetEmployeeWithThirdHighestSalary()
        {
            var employees = await _context.Employees
                .OrderByDescending(e => e.EmployeeSalary)
                .Skip(2) // Skip the first two highest salaries
                .Take(1) // Take one employee
                .FirstOrDefaultAsync();

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }
    }
}