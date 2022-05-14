using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Ui.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [DisplayName("Product Name")]
        [Required(ErrorMessage ="Required Field!")]
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [DisplayName("Unit Price")]
        public decimal? UnitPrice { get; set; }
        public ushort? UnitsInStock { get; set; }
        [DisplayName("Product Picture")]
        [Required(ErrorMessage = "Required Field!")]
        public IFormFile Picture { get; set; }
        public string PictureStr { get; set; }
        [Required]
        public bool Discontinued { get; set; }
    }
}
