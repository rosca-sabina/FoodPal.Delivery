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
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IValidator<UpdateUserCommand> _validator;
        public UpdateUserHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<UpdateUserCommand> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<User>();
            _validator = validator;
        }
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var userModel = _mapper.Map<UpdateUserCommand, User>(request);

            _userRepository.Update(userModel);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
