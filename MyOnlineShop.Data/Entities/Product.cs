using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Data.Entities
{
    public class Product:BaseEntity
    {
        [StringLength(50)]
        [Required]
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public decimal? UnitPrice { get; set; }
        public ushort? UnitsInStock { get; set; }
        [Required]
        public byte[] Picture { get; set; }
        [Required]
        public bool Discontinued { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Category Category { get; set; }
        public List<ProductStore> ProductStores { get; set; }

    }
}
