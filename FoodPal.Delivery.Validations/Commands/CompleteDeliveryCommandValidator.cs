using FluentValidation;
using FoodPal.Delivery.Application.Commands;

namespace FoodPal.Delivery.Validations.Commands
{
    public class CompleteDeliveryCommandValidator: BaseValidator<CompleteDeliveryCommand>
    {
        public CompleteDeliveryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
