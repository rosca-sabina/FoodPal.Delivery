using AutoMapper;
using FluentValidation;
using FoodPal.Delivery.Application.Queries;
using FoodPal.Delivery.Contracts.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Messages
{
    public class GetDeliveriesConsumer : IConsumer<IGetDeliveriesEvent>
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<GetDeliveriesConsumer> _logger;

        public GetDeliveriesConsumer(IMapper mapper, IServiceScopeFactory serviceScopeFactory, ILogger<GetDeliveriesConsumer> logger)
        {
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IGetDeliveriesEvent> context)
        {
            try
            {
                var message = context.Message;
                var query = _mapper.Map<IGetDeliveriesEvent, GetDeliveriesQuery>(message);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var response = await mediator.Send(query);
                    await context.RespondAsync(response);
                }
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {nameof(GetDeliveriesConsumer)}.");
            }
        }
    }
}
