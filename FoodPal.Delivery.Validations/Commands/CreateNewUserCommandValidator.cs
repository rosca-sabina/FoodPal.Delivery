using FluentValidation;
using FoodPal.Delivery.Application.Commands;

namespace FoodPal.Delivery.Validations.Commands
{
    public class CreateNewUserCommandValidator: BaseValidator<CreateNewUserCommand>
    {
        public CreateNewUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.PhoneNo)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Address)
                .MaximumLength(500);
        }
    }
}
