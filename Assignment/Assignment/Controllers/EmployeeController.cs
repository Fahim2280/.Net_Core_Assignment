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
        /*
        public Employee ById(int id)
        {
            return AppDbContext.Employees.Where(x => x.EmployeeId == id).FirstOrDefault;
        }*/

        // PUT: api/Employee/UpdateDetails/5
        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee updateDto)
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

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception
                return StatusCode(500, "An error occurred while saving the changes.");
            }

            return NoContent();
        }
    }
}