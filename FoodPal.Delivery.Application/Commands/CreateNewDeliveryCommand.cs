using MediatR;

namespace FoodPal.Delivery.Application.Commands
{
    public class CreateNewDeliveryCommand: IRequest<bool>
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Info { get; set; }
    }
}
