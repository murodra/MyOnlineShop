using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Data.Entities
{
    public class Customer:BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }
        [Required]
        [StringLength(10)]
        public int Phone { get; set; }
        public List<Order> Orders { get; set; }
        public List<Store> Stores { get; set; }
    }
}
