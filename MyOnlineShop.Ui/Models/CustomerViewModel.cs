using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Ui.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Required field!")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Required field!")]
        public string Lastname { get; set; }
        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "Required field!")]
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Required field!")]
        public string Password { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Phone")]
        [Required(ErrorMessage = "Required field!")]
        [StringLength(10)]
        public int Phone { get; set; }
    }
}
