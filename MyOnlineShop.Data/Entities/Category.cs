using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineShop.Data.Entities
{
    public class Category:BaseEntity
    {
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public int? CategoryParentId { get; set; }

        [ForeignKey("CategoryParentId")]
        public Category CategoryParent { get; set; }
        public List<Category> Categories { get; set; }
        
    }
}
