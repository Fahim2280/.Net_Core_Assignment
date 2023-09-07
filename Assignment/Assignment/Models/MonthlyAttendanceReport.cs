using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class MonthlyAttendanceReport
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string EmployeeName { get; set; }
        [Required]
        public required string MonthName { get; set; }
        [Required]
        public decimal PayableSalary { get; set; }
        [Required]
        public int TotalPresent { get; set; }
        [Required]
        public int TotalAbsent { get; set; }
        [Required]
        public int TotalOffday { get; set; }
    }
}
