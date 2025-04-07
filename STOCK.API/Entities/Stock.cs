namespace STOCK.API.Entities
{
    public class Stock
    {
        public Guid StockID { get; set; }
       
        public Guid ProductID { get; set; }

        public int Count { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}
