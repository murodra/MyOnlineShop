using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineShop.Data.Entities
{
    public class ProductStore
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Store Store { get; set; }
    }
}
