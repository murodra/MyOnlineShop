using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Ui.Models
{
    public class LoginViewModel
    {
        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Required field!")]
        [EmailAddress(ErrorMessage = "sample@mail.com")]
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Required field!")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password length must be between 4 and 8")]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
