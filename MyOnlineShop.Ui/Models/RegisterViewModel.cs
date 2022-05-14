using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Ui.Models
{
    public class RegisterViewModel
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Required field!")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Required field!")]
        public string LastName { get; set; }
        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Required field!")]
        [EmailAddress(ErrorMessage = "sample@mail.com")]
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Required field!")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password length must be between 4 and 8")]
        public string Password { get; set; }
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
