using System.ComponentModel.DataAnnotations;

namespace LiseWoreEmp.Models.ViewModel
{
    public class UserListViewModel
    {
        public short UserId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$",
        ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; } = null!;

        public string UserPassword { get; set; } = null!;
    }
}
