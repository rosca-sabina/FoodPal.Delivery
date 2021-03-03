using AutoMapper;
using FluentValidation;
using FoodPal.Delivery.Application.Queries;
using FoodPal.Delivery.Data.Abstractions;
using FoodPal.Delivery.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Application.Handlers
{
    public class GetDeliveriesHandler : IRequestHandler<GetDeliveriesQuery, DeliveriesDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Models.Delivery> _deliveryRepository;
        private readonly IValidator<GetDeliveriesQuery> _validator;
        public GetDeliveriesHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<GetDeliveriesQuery> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _deliveryRepository = _unitOfWork.GetRepository<Models.Delivery>();
            _validator = validator;
        }
        public async Task<DeliveriesDTO> Handle(GetDeliveriesQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var deliveries = await _deliveryRepository.FindAsync(x => (x.Id == request.Id) || (request.Id == 0 && x.UserId == request.UserId));
            var deliveriesDtoList = _mapper.Map<List<Models.Delivery>, List<DeliveryDTO>>(deliveries.ToList());
            var result = new DeliveriesDTO
            {
                Deliveries = deliveriesDtoList
            };

            return result;
        }
    }
}
