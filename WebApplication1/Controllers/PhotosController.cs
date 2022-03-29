using Business;
using Entitites.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;

        public PhotosController(IUserService userService, IPhotoService photoService)
        {
            _userService = userService;
            _photoService = photoService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm] PhotoForCreation photoDto)
        {
            var user = GetUserById(userId);

            if (user == null)
               return BadRequest("Could not find user");

          var photoForReturn = await _photoService.AddPhotoForUser(userId, photoDto);

            if (photoForReturn != null)
                return Ok();

            return BadRequest("Photo upload failed.");
        }

        [HttpGet("getUser")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUser(id);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("Could not find user");
        }
    }
}
