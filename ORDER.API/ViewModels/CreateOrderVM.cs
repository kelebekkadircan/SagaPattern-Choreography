namespace ORDER.API.ViewModels
{
    public class CreateOrderVM
    {
        public string BuyerID { get; set; }

        public List<CreateOrderItemVM> OrderItems { get; set; } 
    }
}
