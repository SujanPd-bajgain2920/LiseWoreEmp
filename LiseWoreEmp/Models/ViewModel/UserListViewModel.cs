using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiseWoreEmp.Models.ViewModel
{
    public class UserListViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short UserId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$",
         ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; } = null!;

        public string UserPassword { get; set; } = null!;
    }
}
