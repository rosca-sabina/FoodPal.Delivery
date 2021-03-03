namespace FoodPal.Delivery.Contracts.Events
{
    public interface ICreateNewDeliveryEvent
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Info { get; set; }
    }
}
