using AutoMapper;
using FluentValidation;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Data.Abstractions;
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
        private readonly IValidator<CreateNewDeliveryCommand> _validator;
        public CreateNewDeliveryHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<CreateNewDeliveryCommand> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _deliveryRepository = _unitOfWork.GetRepository<Models.Delivery>();
            _validator = validator;
        }

        public async Task<bool> Handle(CreateNewDeliveryCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var deliveryModel = _mapper.Map<CreateNewDeliveryCommand, Models.Delivery>(request);
            deliveryModel.CreatedAt = DateTimeOffset.Now;
            deliveryModel.ModifiedAt = DateTimeOffset.Now;

            await _deliveryRepository.CreateAsync(deliveryModel);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
