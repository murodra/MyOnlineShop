using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Ui.Models
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        [DisplayName("Store Name")]
        [Required(ErrorMessage = "Required field!")]
        public string StoreName { get; set; }
        [DisplayName("Address")]
        [Required(ErrorMessage = "Required field!")]
        public string Address { get; set; }
        public string City { get; set; }
        public IFormFile StoreLogo { get; set; }
        public string PictureStr { get; set; }
    }
}
