using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Data.Entities
{
    public class Store:BaseEntity
    {
        [Required]
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public byte[] StoreLogo { get; set; }
        public Customer Customer { get; set; }
        public List<Order> Orders { get; set; }
        public List<ProductStore> ProductStores { get; set; }
    }
}
