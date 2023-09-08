using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Assignment.DTO;

namespace Assignment.Controllers
{
    [Route("api/EmployeeAttendanceReport")]
    [ApiController]
    public class EmployeeAttendanceReportController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeAttendanceReportController(AppDbContext context)
        {
            _context = context;
        }


        //API05#
        [HttpGet("monthlyattendancereport")]
        public async Task<ActionResult<IEnumerable<MonthlyAttendanceReportDto>>> GetMonthlyAttendanceReport()
        {
            var monthlyReport = await _context.EmployeeAttendances
                .Where(a => a.IsPresent || a.IsAbsent || a.IsOffday)
                .GroupBy(a => new { a.Employee.EmployeeName, a.AttendanceDate.Month })
                .Select(g => new MonthlyAttendanceReportDto
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
