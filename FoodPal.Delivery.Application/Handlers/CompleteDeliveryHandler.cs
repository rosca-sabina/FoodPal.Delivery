using FluentValidation;
using FoodPal.Contracts;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Data.Abstractions;
using FoodPal.Delivery.Enums;
using FoodPal.Notifications.Common.Enums;
using MassTransit;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Application.Handlers
{
    public class CompleteDeliveryHandler : IRequestHandler<CompleteDeliveryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Models.Delivery> _deliveryRepository;
        private readonly IRepository<Models.User> _userRepository;
        private readonly IValidator<CompleteDeliveryCommand> _validator;
        private readonly IPublishEndpoint _publishEndpoint;
        public CompleteDeliveryHandler(IUnitOfWork unitOfWork, IValidator<CompleteDeliveryCommand> validator, IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _deliveryRepository = _unitOfWork.GetRepository<Models.Delivery>();
            _userRepository = _unitOfWork.GetRepository<Models.User>();
            _validator = validator;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<bool> Handle(CompleteDeliveryCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var deliveryModel = await _deliveryRepository.FindByIdAsync(request.Id);
            if(deliveryModel is null)
            {
                throw new ArgumentException($"No delivery with id {request.Id} exists.");
            }

            deliveryModel.Status = DeliveryStatus.Completed;
            deliveryModel.ModifiedAt = DateTimeOffset.Now;

            var saved =  await _unitOfWork.SaveChangesAsync();

            var userModel = await _userRepository.FindByIdAsync(deliveryModel.UserId);
            await _publishEndpoint.Publish<INewNotificationAdded>(new
            {
                Title = "Delivery Completed",
                Message = "Delivery Completed",
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
