using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;

namespace WebApp.Web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public AuthController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            var registerStatus = await _userService.TryRegister(user);

            if (registerStatus.ServiceResultType == ServiceResultType.Error)
            {
                return BadRequest("Invalid Register Attempt");
            }

            var confirmationLink = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/auth/confirm?email={user.Email}&token={registerStatus.Result}";
            bool emailResponse = await _emailService.SendEmailAsync(user.Email, "Confirm Email", confirmationLink);

            if (!emailResponse)
            {
                return BadRequest("Invalid Confirm Email Attempt");
            }

            return Created(new Uri("api/home/info", UriKind.Relative), null);

        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var isConfirmed = await _userService.ConfirmEmail(email, token);

            if (isConfirmed.ServiceResultType == ServiceResultType.Success)
            {
                return Ok();
            }

            return BadRequest(isConfirmed.Message);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> Login(UserDTO user)
        {
            if(!ModelState.IsValid)
            {
                return Unauthorized();
            }

            bool tryLogin = await _userService.TryLogin(user);

            if (!tryLogin)
            {
                return Unauthorized();
            }

            return Ok();
        }

    }
}
