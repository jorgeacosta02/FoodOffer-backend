using FoodOffer.AppServices;
using FoodOffer.Model.Models;
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

        public IActionResult GetAdvertising([FromQuery] short advId)
        {
            var user = _advertisingService.GetAdvertising(advId);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("No se encontró publicación para el ID de la búsqueda.");
            }
        }

        [HttpPost]
        [Route("GetAdvertisings")]
        public IActionResult GetAdvertisings([FromBody] Filter filter)
        {
            var advs = _advertisingService.GetAdvertisings(filter);

            if (advs != null && advs.Count > 0)
            {
                return Ok(advs);
            }
            else
            {
                return BadRequest("No se encontraron publicaciones activas.");
            }
        }

        [HttpPost]
        [Route("CreateAdvertising")]
        public IActionResult CreateAdvertising([FromForm] IFormCollection data)
        {
            try
            {
                var adv = new Advertising();

                adv.Commerce.Id = int.Parse(data["com_id"]);
                adv.Title = data["title"];
                adv.Description = data["description"];
                adv.Price = int.Parse(data["price"]);
                adv.Category.Code = short.Parse(data["category"]);
                adv.State.Code = 'A';

                var images = data.Files.GetFiles("images");
                foreach (var img in images)
                {
                    var newImg = new Image();
                    newImg.ImageFile = img;
                    newImg.Item = 1; //TODO -- short.Parse(img.FileName.Split("-")[0]);
                    adv.Images.Add(newImg);
                }

                return Ok(_advertisingService.CreateAdvertising(adv));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("UpdateAdvertisingData")]
        public IActionResult UpdateAdvertising([FromForm] IFormCollection data)
        {
            try
            {
                var adv = new Advertising();
                adv.Id = int.Parse(data["id"]);
                adv.Commerce.Id = int.Parse(data["com_id"]);
                adv.Title = data["title"];
                adv.Description = data["description"];
                adv.Price = int.Parse(data["price"]);
                adv.Category.Code = short.Parse(data["category"]);
                adv.State.Code = char.Parse(data["state"]); ;

                var images = data.Files.GetFiles("images");

                if(images != null && images.Count > 0)
                {
                    foreach (var img in images)
                    {
                        var newImg = new Image();
                        newImg.ImageFile = img;
                        newImg.Item = 1; //TODO -- short.Parse(img.FileName.Split("-")[0]);
                        adv.Images.Add(newImg);
                    }
                }


                return Ok(_advertisingService.UpdateAdvertising(adv));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateAdvertisingState")]
        public IActionResult UpdateAdvertisingState([FromQuery] int id, [FromQuery] char state)
        {
            try
            {
                var adv = new Advertising();
                adv.Id = id;
                adv.State.Code = state;

                return Ok(_advertisingService.UpdateAdvertisingState(adv));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("DeleteAdvertising")]
        public IActionResult DeleteAdvertising([FromQuery] int id)
        {
            try
            {
                return Ok(_advertisingService.DeleteAdvertising(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
