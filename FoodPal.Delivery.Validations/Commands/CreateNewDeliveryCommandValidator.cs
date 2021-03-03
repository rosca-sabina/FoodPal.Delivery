using FluentValidation;
using FoodPal.Delivery.Application.Commands;

namespace FoodPal.Delivery.Validations.Commands
{
    public class CreateNewDeliveryCommandValidator: BaseValidator<CreateNewDeliveryCommand>
    {
        public CreateNewDeliveryCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.UserId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.CreatedBy)
                .NotEmpty();

            RuleFor(x => x.Info)
                .MaximumLength(1000);
        }
    }
}
