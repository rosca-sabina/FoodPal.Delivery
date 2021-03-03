namespace FoodPal.Delivery.Contracts.Events
{
    public interface IGetDeliveriesEvent
    {
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
