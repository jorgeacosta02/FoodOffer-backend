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

        #region Get Advertisings

        /// <summary>
        /// Get active advertisings by advertising timeset and actual datetime.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Return an active advertisings list</returns>

        [HttpPost]
        [Route("GetAdvertisings")]
        public IActionResult GetAdvertisings([FromBody] AdvFilter filter)
        {
            var advs = _advertisingService.GetAdvertisings(filter);

            return Ok(advs);

        }

        /// <summary>
        /// Get advertising by Id.
        /// </summary>
        /// <param name="advId">Advertising id number</param>
        /// <returns>If advertising exists, return advertising. Otherwise return null</returns>

        [HttpGet]
        [Route("GetAdvertising")]

        public IActionResult GetAdvertising([FromQuery] int advId)
        {
            var adv = _advertisingService.GetAdvertisingDetail(advId);

            if (adv != null)
            {
                return Ok(adv);
            }
            else
            {
                return BadRequest("No se encontró publicación para el ID de la búsqueda.");
            }
        }

        [HttpGet]
        [Route("GetAdvertisingsByCommerce")]

        public IActionResult GetAdvertisingByCommerce([FromQuery] short comId)
        {
            var adv = _advertisingService.GetAdvertisingsByCommerce(comId);

            if (adv != null)
            {
                return Ok(adv);
            }
            else
            {
                return BadRequest("No se encontró publicación para el ID de la búsqueda.");
            }
        }


        #endregion

        # region Advertising edition


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
                adv.CategoryCode = short.Parse(data["category"]);
                adv.PriorityLevel = short.Parse(data["priority"]);
                adv.StateCode = 1; // 1 = Active.

                short item = 1;

                var images = data.Files.GetFiles("images");

                foreach (var img in images)
                {
                    var newImage = new AppImage();
                    newImage.ImageFile = img;
                    newImage.Item = item;
                    newImage.Name = adv.Title;
                    adv.Images.Add(newImage);
                    item++;
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
        public IActionResult UpdateAdvertisingData([FromBody] Advertising adv)
        {
            try
            {
                return Ok(_advertisingService.UpdateAdvertisingData(adv));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateAdvertisingImages")]
        public IActionResult UpdateAdvertisingImages([FromForm] IFormCollection data)
        {
            try
            {
                var advId = int.Parse(data["id"]);
                var comId = int.Parse(data["com_id"]);

                var adv = _advertisingService.GetAdvertisingSimple(advId);

                if (adv == null)
                    throw new Exception("Advertising not found");

                if (adv.Commerce.Id != comId)
                    throw new Exception("Error: Advertisign commerce Id doesn't match with request data");

                var images = data.Files.GetFiles("images");

                short item = 1;

                if (images != null && images.Count > 0)
                {
                    foreach (var img in images)
                    {
                        var newImg = new AppImage();
                        newImg.ImageFile = img;
                        newImg.Item = item;
                        newImg.Name = adv.Title;
                        adv.Images.Add(newImg);
                        item++;
                    }

                    return Ok(_advertisingService.UpdateAdvertisingImages(adv));

                }
                else 
                {
                    return BadRequest("No images were included on the request");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateAdvertisingState")]
        public IActionResult UpdateAdvertisingState([FromQuery] int id, [FromQuery] short state)
        {
            try
            {
                var adv = new Advertising();
                adv.Id = id;
                adv.StateCode = state;

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

        #endregion

        [HttpGet]
        [Route("GetAdvertisingSetting")]

        public IActionResult GetAdvertisingSetting([FromQuery] short advId)
        {
            var adv = _advertisingService.GetAdvertisingSetting(advId);

            if (adv != null)
            {
                return Ok(adv);
            }
            else
            {
                return BadRequest("No se encontró publicación para el ID de la búsqueda.");
            }
        }

        [HttpPost]
        [Route("SetAdvertisingTimeSet")]
        public IActionResult GetAdvertisings([FromBody] List<AdvertisingTimeSet> timeSets)
        {
             
             int advId = timeSets.First().AdvId;

            var result = _advertisingService.SetAdvertisingTimeSet(timeSets, advId);

            return Ok(result);

        }

    }
}
