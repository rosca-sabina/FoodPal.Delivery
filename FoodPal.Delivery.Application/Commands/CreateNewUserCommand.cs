using MediatR;

namespace FoodPal.Delivery.Application.Commands
{
    public class CreateNewUserCommand: IRequest<bool>
    {
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
