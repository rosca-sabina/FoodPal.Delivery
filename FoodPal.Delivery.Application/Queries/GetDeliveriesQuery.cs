using FoodPal.Delivery.DTOs;
using MediatR;

namespace FoodPal.Delivery.Application.Queries
{
    public class GetDeliveriesQuery: IRequest<DeliveriesDTO>
    {
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
