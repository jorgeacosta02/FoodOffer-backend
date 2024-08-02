using FoodOffer.Model.Models;
using FoodOffer.Model.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOffer.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
  
        private readonly ILogger<UserController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<UserController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        [HttpGet]
        [Route("GetCategories")]
        public IActionResult GetCategories([FromQuery] short type)
        {
            try
            {
                var categories = _categoryService.GetCategories(type);

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAttibutes")]
        public IActionResult GetAttributes([FromQuery] short type)
        {
            try
            {
                var attributes = _categoryService.GetAttibutesByCategory(type);

                return Ok(attributes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddCategory")]
        public IActionResult AddCategory([FromBody] Category data)
        {
            try
            {
                Category category = _categoryService.AddCategory(data, data.Type);

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateCategory")]
        public IActionResult UpdateCategory([FromBody] Category data, [FromQuery] short type)
        {
            try
            {
                Category category = _categoryService.UpdateCategory(data, type);

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteCategory")]
        public IActionResult DeleteCategory([FromQuery] int id, [FromQuery] short type)
        {
            try
            {
                //Category category = _categoryService.UpdateCategory(data, type);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}