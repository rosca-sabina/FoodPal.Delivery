using MediatR;

namespace FoodPal.Delivery.Application.Commands
{
    public class CompleteDeliveryCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
}
