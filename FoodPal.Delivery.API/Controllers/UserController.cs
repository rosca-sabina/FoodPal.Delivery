using FoodPal.Delivery.Contracts.Events;
using FoodPal.Delivery.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoodPal.Delivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public UserController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser(NewUserDTO newUserDTO)
        {
            await _publishEndpoint.Publish<ICreateNewUserEvent>(newUserDTO);
            return Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            await _publishEndpoint.Publish<IUpdateUserEvent>(updateUserDTO);
            return Accepted();
        }
    }
}
