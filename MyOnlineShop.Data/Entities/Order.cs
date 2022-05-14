using System;
using System.Collections.Generic;

namespace MyOnlineShop.Data.Entities
{
    public class Order:BaseEntity
    {
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Customer Customer { get; set; }
        public Store Store { get; set; }
    }
}
