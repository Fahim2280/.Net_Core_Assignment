using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models
{
    public class EmployeeAttendance
    {
        [Key]
        public int EmployeeAttendanceId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime AttendanceDate { get; set; }
        [Required]
        public int IsPresent { get; set; }
        [Required]
        public int IsAbsent { get; set; }
        [Required]
        public int IsOffday { get; set; }


        public virtual Employee? Employee { get; set; }
    }
}
