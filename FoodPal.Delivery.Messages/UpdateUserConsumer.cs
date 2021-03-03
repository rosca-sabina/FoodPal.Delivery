using AutoMapper;
using FluentValidation;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Contracts.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Messages
{
    public class UpdateUserConsumer : IConsumer<IUpdateUserEvent>
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<UpdateUserConsumer> _logger;

        public UpdateUserConsumer(IMapper mapper, IServiceScopeFactory serviceScopeFactory, ILogger<UpdateUserConsumer> logger)
        {
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IUpdateUserEvent> context)
        {
            try
            {
                var message = context.Message;
                var command = _mapper.Map<IUpdateUserEvent, UpdateUserCommand>(message);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(command);
                }
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {nameof(UpdateUserConsumer)}.");
            }
        }
    }
}
