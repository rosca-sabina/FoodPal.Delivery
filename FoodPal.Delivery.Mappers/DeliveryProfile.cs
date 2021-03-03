using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Application.Queries;
using FoodPal.Delivery.Contracts.Events;
using FoodPal.Delivery.DTOs;

namespace FoodPal.Delivery.Mappers
{
    public class DeliveryProfile: BaseProfile
    {
        public DeliveryProfile()
        {
            CreateMap<ICreateNewDeliveryEvent, CreateNewDeliveryCommand>();
            CreateMap<CreateNewDeliveryCommand, Models.Delivery>();
            CreateMap<ICompleteDeliveryEvent, CompleteDeliveryCommand>();
            CreateMap<IGetDeliveriesEvent, GetDeliveriesQuery>();
            CreateMap<Models.Delivery, DeliveryDTO>();
        }
    }
}
