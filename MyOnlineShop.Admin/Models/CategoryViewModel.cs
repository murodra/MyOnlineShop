using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Admin.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [DisplayName("Choose the parent category")]
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public int? GrandParentId { get; set; }
        [Required(ErrorMessage ="Required field!")]
        public string CategoryName { get; set; }
        public string ParentCategoryName { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        public string PictureStr { get; set; }
    }
}
