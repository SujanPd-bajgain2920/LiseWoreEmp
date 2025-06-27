using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiseWoreEmp.Models.ViewModel
{
    public class EmployeeViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short EmployeeId { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;


        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$",
        ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\+977[0-9]{10}$",
       ErrorMessage = "Phone number must start with +977 followed by 10 digits")]
        public string Phone { get; set; } = null!;

        public string? Department { get; set; }

        public string? Position { get; set; }

        public DateOnly? HireDate { get; set; }

        public string AddedBy { get; set; }

        public string EmpEncId { get; set; }
    }
}
