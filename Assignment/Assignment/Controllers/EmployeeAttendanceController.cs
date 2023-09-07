﻿using Assignment.Models;
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


        [HttpGet("monthlyattendancereport")]
        public async Task<ActionResult<IEnumerable<MonthlyAttendanceReport>>> GetMonthlyAttendanceReport()
        {
            var monthlyReport = await _context.EmployeeAttendances
                .Where(a => a.IsPresent || a.IsAbsent || a.IsOffday)
                .GroupBy(a => new { a.Employee.EmployeeName, a.AttendanceDate.Month })
                .Select(g => new MonthlyAttendanceReport
                {
                    EmployeeName = g.Key.EmployeeName,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                    PayableSalary = g.First().Employee.EmployeeSalary,
                    TotalPresent = g.Count(a => a.IsPresent),
                    TotalAbsent = g.Count(a => a.IsAbsent),
                    TotalOffday = g.Count(a => a.IsOffday)
                })
                .ToListAsync();

            if (monthlyReport == null || monthlyReport.Count == 0)
            {
                return NotFound();
            }

            return monthlyReport;
        }


    }
}

