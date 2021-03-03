using AutoMapper;
using FluentValidation;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Data.Abstractions;
using FoodPal.Delivery.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Application.Handlers
{
    public class CreateNewUserHandler : IRequestHandler<CreateNewUserCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IValidator<CreateNewUserCommand> _validator;
        public CreateNewUserHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<CreateNewUserCommand> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<User>();
            _validator = validator;
        }

        public async Task<bool> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var userModel = _mapper.Map<CreateNewUserCommand, User>(request);
            await _userRepository.CreateAsync(userModel);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
