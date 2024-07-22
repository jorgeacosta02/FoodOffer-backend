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

        [HttpPost]
        [Route("add")]
        public IActionResult AddCategory([FromBody] Category data, [FromQuery] short type)
        {
            try
            {
                Category category = _categoryService.AddCategory(data, type);

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update")]
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

    }
}