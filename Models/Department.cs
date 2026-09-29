using System.ComponentModel.DataAnnotations;

namespace WorkFlowHR.Models
{
    public class Department
    {
        [Required(ErrorMessage = "Department is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a department")]
        public int Id { get; set; }

        public string Name { get; set; }


    }
}
