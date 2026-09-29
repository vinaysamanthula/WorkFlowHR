using System.ComponentModel.DataAnnotations;

namespace WorkFlowHR.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        
        public string Designation { get; set; }
        [Range(1, int.MaxValue,
    ErrorMessage = "Please select a department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
