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
        private readonly IEmailService _emailService;
        private readonly IUrlHelper _urlHelper;

        public AuthController(IUserService userService, IEmailService emailService, IUrlHelper urlHelper)
        {
            _userService = userService;
            _emailService = emailService;
            _urlHelper = urlHelper;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            if(ModelState.IsValid)
            {
                string tryRegister = await _userService.TryRegister(user);
                if(tryRegister != null)
                {

                    var confirmationLink = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/auth/confirm?email={user.Email}&token={tryRegister}";
                    bool emailResponse = await _emailService.SendEmailAsync(user.Email, "Confirm Email", confirmationLink);

                    if(emailResponse)
                        return Created(new Uri("api/home/info", UriKind.Relative), null);
                }
            }

            return BadRequest("Invalid Register Attempt");
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            bool IsConfirmed = await _userService.ConfirmEmail(email, token); ;

            if (IsConfirmed)
                return Ok();

            return BadRequest();
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
