using FoodOffer.Model.Models;
using FoodOffer.Model.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOffer.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public IActionResult Get([FromQuery] short userId)
        {
            User user = _userService.GetUser(userId);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("El usuario con Id = " + userId + " no existe. Revise los datos.");
            }
        }

        [HttpPost]
        [Route("addUser")]
        public IActionResult Post([FromBody] User user)
        {
            User usr = _userService.PostUser(user);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("No se puede crear el usuario.");
            }
        }
    }
}