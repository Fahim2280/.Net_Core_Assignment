using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;


namespace Assignment.Controllers
{
    [Route("api/EmployeeAttendance")]
    [ApiController]
    public class EmployeeAttendanceController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeAttendanceController(AppDbContext context)
        {
            _context = context;
        }
        
        //API03#
        [HttpGet("maxsalarynoabsent")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesMaxSalaryNoAbsent()
        {
            var employees = await _context.Employees
                .Where(e => !e.EmployeeAttendances.Any(a => a.IsAbsent))
                .OrderByDescending(e => e.EmployeeSalary)
                .Select(e => new Employee
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    EmployeeCode = e.EmployeeCode,
                    EmployeeSalary = e.EmployeeSalary,
                    SupervisorId = e.SupervisorId,
                    EmployeeAttendances = e.EmployeeAttendances
                })
                .ToListAsync();

            if (employees == null || employees.Count == 0)
            {
                return NotFound();
            }

            return employees;
        }
        
    }
}

