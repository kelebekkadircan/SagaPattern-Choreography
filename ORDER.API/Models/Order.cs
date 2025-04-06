using ORDER.API.Enums;

namespace ORDER.API.Models
{
    public class Order
    {
        public Guid OrderID { get; set; }

        public Guid BuyerID { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public OrderStatusEnum OrderStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal TotalPrice { get; set; } 
    }
}
