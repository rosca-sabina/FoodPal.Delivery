using FluentValidation;
using FoodPal.Delivery.Application.Handlers;
using FoodPal.Delivery.Data;
using FoodPal.Delivery.Data.Abstractions;
using FoodPal.Delivery.Mappers;
using FoodPal.Delivery.Messages;
using FoodPal.Delivery.Validations;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Processor
{
    public class Program
    {

        static async Task Main(string[] args)
        {
            await Host
                .CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .ConfigureServices(ConfigureServices)
                .RunConsoleAsync();
        }

        private static void ConfigureServices(HostBuilderContext hostBuilder, IServiceCollection services)
        {
            services.AddHostedService<MassTransitHostedService>();

            // FluentValidation
            services.AddValidatorsFromAssembly(typeof(BaseValidator<>).Assembly);

            // AutoMapper
            services.AddAutoMapper(typeof(BaseProfile).Assembly);

            // MediatR
            services.AddMediatR(typeof(CreateNewUserHandler).Assembly);

            // Database access
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDataAccessConfiguration(hostBuilder.Configuration);

            // Consumers
            services.AddScoped<CreateNewUserConsumer>();
            services.AddScoped<UpdateUserConsumer>();
            services.AddScoped<CreateNewDeliveryConsumer>();
            services.AddScoped<CompleteDeliveryConsumer>();
            services.AddScoped<GetDeliveriesConsumer>();

            // MassTransit
            string serviceBusHost = hostBuilder.Configuration.GetSection("MessageBroker").GetValue<string>("ServiceBusHost");
            services.AddMassTransit(configuration =>
            {
                // configuration.AddConsumer<CreateNewUserConsumer>();

                configuration.UsingAzureServiceBus((context, config) =>
                {
                    config.Host(serviceBusHost);

                    config.ReceiveEndpoint("users-queue", e =>
                    {
                        e.Consumer(() => context.GetService<CreateNewUserConsumer>());
                        e.Consumer(() => context.GetService<UpdateUserConsumer>());
                    });

                    config.ReceiveEndpoint("delivery-queue", e =>
                    {
                        e.Consumer(() => context.GetService<CreateNewDeliveryConsumer>());
                        e.Consumer(() => context.GetService<CompleteDeliveryConsumer>());
                        e.Consumer(() => context.GetService<GetDeliveriesConsumer>());
                    });
                });
            });
        }
    }
}
