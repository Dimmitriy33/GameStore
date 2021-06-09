using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Services;

namespace WebApp.Web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IUserService UserServiceProp
        {
            get => new UserService();
        }

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            if(ModelState.IsValid)
            {
                bool tryRegister = await UserServiceProp.TryRegister(user, _userManager, _signInManager);
                if(tryRegister)
                    return CreatedAtAction("info", "Home");
            }

            return BadRequest("Invalid Register Attempt");
        }



        [HttpGet]
        [AllowAnonymous]
        [Route("sign-up")]
        public IActionResult Register()
        {
            return Created(new Uri("api/home/info", UriKind.Relative), null);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("sign-in")]
        public async Task<IActionResult> Login(UserDTO user)
        {
            if(ModelState.IsValid)
            {
                bool tryLogin = await UserServiceProp.TryLogin(user, _userManager, _signInManager);

                if(tryLogin)
                    return CreatedAtAction("info", "Home");
            }

            return Unauthorized(null);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("sign-in")]
        public IActionResult Login()
        {
            return Ok("Successful authentication!");
        }
    }
}
