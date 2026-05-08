namespace EMS.Models
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }

        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<LeaveRequest>? LeaveRequests { get; set; }

        public string? Designation { get; set; }

        public decimal Salary { get; set; }

        public DateTime JoiningDate { get; set; }
    }
}
