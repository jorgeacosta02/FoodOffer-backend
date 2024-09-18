using FoodOffer.AppServices;
using FoodOffer.Model.Models;
using FoodOffer.Model.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOffer.WebAPI.Controllers
{
    public class CommerceController : ControllerBase
    {
        private readonly ICommerceService _commerceService;

        public CommerceController(ICommerceService commerceService)
        {
            _commerceService = commerceService ?? throw new ArgumentNullException(nameof(commerceService));
        }

        [HttpGet]
        [Route("GetCommerces")]

        public IActionResult GetAllCommerces()
        {
            try
            {
                return Ok(_commerceService.GetCommerces());

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }

        [HttpGet]
        [Route("GetCommerce")]

        public IActionResult GetCommerce([FromQuery] int comId)
        {
            try
            {
                var com = _commerceService.GetCommerceComplete(comId);

                if (com != null)
                {
                    return Ok(com);
                }
                else
                {
                    return BadRequest($"The commerce ID: {comId} wasn't found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }


        [HttpPost]
        [Route("AddCommerce")]
        public IActionResult AddCommerce([FromBody] Commerce data)
        {
            try
            {
                Commerce commerce = _commerceService.AddCommerce(data);

                return Ok(commerce);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SaveCommerceImage")]
        public IActionResult CreateAdvertising([FromForm] IFormCollection data)
        {
            try
            {
                var comId = int.Parse(data["com_id"]);
                var image = data.Files.GetFile("image");

                _commerceService.SaveCommerceImage(comId, image);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
