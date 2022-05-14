using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Admin.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage ="Required field!")]
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal? UnitPrice { get; set; }
        public ushort? UnitsInStock { get; set; }
        [Required(ErrorMessage ="Required field!")]
        public IFormFile Picture { get; set; }
        public string PictureStr { get; set; }
        [Required]
        public bool Discontinued { get; set; }
    }
}
