namespace FoodPal.Delivery.DTOs
{
    public class NewDeliveryDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Info { get; set; }
    }
}
