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
    public class CreateNewUserConsumer : IConsumer<ICreateNewUserEvent>
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<CreateNewUserConsumer> _logger;

        public CreateNewUserConsumer(IMapper mapper, IServiceScopeFactory serviceScopeFactory, ILogger<CreateNewUserConsumer> logger)
        {
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ICreateNewUserEvent> context)
        {
            try
            {
                var message = context.Message;
                var command = _mapper.Map<ICreateNewUserEvent, CreateNewUserCommand>(message);

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
                _logger.LogError(ex, $"Error occured in {nameof(CreateNewUserConsumer)}.");
            }
        }
    }
}
