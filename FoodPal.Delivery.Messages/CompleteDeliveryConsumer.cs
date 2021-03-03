using AutoMapper;
using FluentValidation;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Contracts.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Messages
{
    public class CompleteDeliveryConsumer : IConsumer<ICompleteDeliveryEvent>
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<CompleteDeliveryConsumer> _logger;

        public CompleteDeliveryConsumer(IMapper mapper, IServiceScopeFactory serviceScopeFactory, ILogger<CompleteDeliveryConsumer> logger)
        {
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ICompleteDeliveryEvent> context)
        {
            try
            {
                var message = context.Message;
                var command = _mapper.Map<ICompleteDeliveryEvent, CompleteDeliveryCommand>(message);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(command);
                }
            }
            catch(ValidationException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (Exception ex){
                _logger.LogError(ex, $"Error occured in {nameof(CompleteDeliveryConsumer)}.");
            }
        }
    }
}
