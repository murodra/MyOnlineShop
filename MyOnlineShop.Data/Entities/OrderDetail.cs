namespace MyOnlineShop.Data.Entities
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public ushort Quantity { get; set; }
        public float Discount { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
