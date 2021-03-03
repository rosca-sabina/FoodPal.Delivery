using FluentValidation;
using FoodPal.Delivery.Application.Queries;

namespace FoodPal.Delivery.Validations.Queries
{
    public class GetDeliveriesQueryValidator: BaseValidator<GetDeliveriesQuery>
    {
        public GetDeliveriesQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
