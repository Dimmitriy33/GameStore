using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;

namespace WebApp.Web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService )
        {
            _userService = userService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            if(ModelState.IsValid)
            {
                bool tryRegister = await _userService.TryRegister(user);
                if(tryRegister)
                    return Created(new Uri("api/home/info", UriKind.Relative), null);
            }

            return BadRequest("Invalid Register Attempt");
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> Login(UserDTO user)
        {
            if(ModelState.IsValid)
            {
                bool tryLogin = await _userService.TryLogin(user);

                if(tryLogin)
                    return Ok("Successful authentication!");
            }

            return Unauthorized(null);
        }

    }
}
