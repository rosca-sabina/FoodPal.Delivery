using FoodPal.Delivery.Contracts.Events;
using FoodPal.Delivery.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPal.Delivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<IGetDeliveriesEvent> _deliveriesRequestClient;
        public DeliveryController(IPublishEndpoint publishEndpoint, IRequestClient<IGetDeliveriesEvent> deliveriesRequestClient)
        {
            _publishEndpoint = publishEndpoint;
            _deliveriesRequestClient = deliveriesRequestClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewDelivery(NewDeliveryDTO newDeliveryDTO)
        {
            await _publishEndpoint.Publish<ICreateNewDeliveryEvent>(newDeliveryDTO);
            return Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> CompleteDelivery(CompleteDeliveryDTO completeDeliveryDTO)
        {
            await _publishEndpoint.Publish<ICompleteDeliveryEvent>(completeDeliveryDTO);
            return Accepted();
        }

        [HttpGet]
        public async Task<IActionResult> GetDeliveries(int userId, int deliveryId)
        {
            try
            {
                var getDeliveriesDTO = new GetDeliveriesDTO
                {
                    Id = deliveryId,
                    UserId = userId
                };
                var response = await _deliveriesRequestClient.GetResponse<DeliveriesDTO>(getDeliveriesDTO);

                return Ok(response.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured.");
            }
        }
    }
}
