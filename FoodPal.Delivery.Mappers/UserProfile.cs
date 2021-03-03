using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Contracts.Events;
using FoodPal.Delivery.Models;

namespace FoodPal.Delivery.Mappers
{
    public class UserProfile: BaseProfile
    {
        public UserProfile()
        {
            CreateMap<ICreateNewUserEvent, CreateNewUserCommand>();
            CreateMap<CreateNewUserCommand, User>();

            CreateMap<IUpdateUserEvent, UpdateUserCommand>();
            CreateMap<UpdateUserCommand, User>();
        }
    }
}
