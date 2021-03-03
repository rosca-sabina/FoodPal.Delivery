using AutoMapper;
using FluentValidation;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Data.Abstractions;
using FoodPal.Delivery.Enums;
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
        private readonly IValidator<CompleteDeliveryCommand> _validator;
        public CompleteDeliveryHandler(IUnitOfWork unitOfWork, IValidator<CompleteDeliveryCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _deliveryRepository = _unitOfWork.GetRepository<Models.Delivery>();
            _validator = validator;
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

            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
