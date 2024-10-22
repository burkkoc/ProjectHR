using IKProject.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IKProject.WebApii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        //[HttpGet]
        //public IActionResult GetUserById(Guid Id)
        //{
        //    return Ok();
        //    //return Ok(_userService.GetUserById());
        //}
    }
}
