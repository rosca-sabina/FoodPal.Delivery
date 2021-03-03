namespace FoodPal.Delivery.Contracts.Events
{
    public interface ICreateNewUserEvent
    {
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
