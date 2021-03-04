using AutoMapper;
using FluentValidation;
using FoodPal.Contracts;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Data.Abstractions;
using FoodPal.Notifications.Common.Enums;
using MassTransit;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Application.Handlers
{
    public class CreateNewDeliveryHandler : IRequestHandler<CreateNewDeliveryCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Models.Delivery> _deliveryRepository;
        private readonly IRepository<Models.User> _userRepository;
        private readonly IValidator<CreateNewDeliveryCommand> _validator;
        private readonly IPublishEndpoint _publishEndpoint;
        public CreateNewDeliveryHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<CreateNewDeliveryCommand> validator, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _deliveryRepository = _unitOfWork.GetRepository<Models.Delivery>();
            _userRepository = _unitOfWork.GetRepository<Models.User>();
            _validator = validator;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<bool> Handle(CreateNewDeliveryCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var deliveryModel = _mapper.Map<CreateNewDeliveryCommand, Models.Delivery>(request);
            deliveryModel.CreatedAt = DateTimeOffset.Now;
            deliveryModel.ModifiedAt = DateTimeOffset.Now;

            await _deliveryRepository.CreateAsync(deliveryModel);
            var saved = await _unitOfWork.SaveChangesAsync();

            var userModel = await _userRepository.FindByIdAsync(deliveryModel.UserId);
            await _publishEndpoint.Publish<INewNotificationAdded>(new { 
                Title = "New delivery",
                Message = "New delivery",
                UserId = userModel.Id,
                CreatedBy = $"{userModel.FirstName} {userModel.LastName}",
                ModifiedBy = $"{userModel.FirstName} {userModel.LastName}",
                Type = NotificationTypeEnum.InApp,
                Status = NotificationStatusEnum.Created
            });

            return saved;
        }
    }
}
