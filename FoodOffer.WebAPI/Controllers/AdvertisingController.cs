using FoodOffer.AppServices;
using FoodOffer.Model.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOffer.WebAPI.Controllers
{
    public class AdvertisingController: ControllerBase
    {
        private readonly IAdvertisingService _advertisingService;
        public AdvertisingController(IAdvertisingService advertisingService) {
            _advertisingService = advertisingService;
        }

        [HttpGet]
        [Route("GetAdvertising")]
        public IActionResult GetAdvertising([FromQuery] short userId)
        {
            var user = _advertisingService.GetAdvertising(userId);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("El usuario con Id = " + userId + " no existe. Revise los datos.");
            }
        }
    }
}
