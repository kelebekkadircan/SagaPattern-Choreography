namespace ORDER.API.Models
{
    public class OrderItem
    {
        public Guid OrderItemID { get; set; }

        public Guid ProductID { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

    }
}
