using MyOnlineShop.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Data.Entities
{
    public class User:BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Title? Title { get; set; }
    }
}
