﻿using FoodOffer.AppServices;
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
        [Route("GetCommerce")]

        public IActionResult GetAdvertising([FromQuery] int advId)
        {
            //var adv = _commerceService.(advId);

            //if (adv != null)
            //{
            //    return Ok(adv);
            //}
            //else
            //{
            //    return BadRequest("No se encontró publicación para el ID de la búsqueda.");
            //}
            return Ok();
        }


    }
}
