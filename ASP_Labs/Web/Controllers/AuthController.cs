using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        #region Constants

        private static string InvalidRegisterMessage = "Invalid Register Attempt";
        private static string InvalidConfirmEmailMessage = "Invalid Confirm Email Attempt";

        #endregion


        public AuthController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register([BindRequired] AuthUserDTO user)
        {
            var registerStatus = await _userService.TryRegisterAsync(user);

            if (registerStatus.ServiceResultType != ServiceResultType.Success)
            {
                return BadRequest(InvalidRegisterMessage);
            }

            var confirmationLink = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/auth/confirm?email={user.Email}&token={registerStatus.Result}";
            var emailResponse = await _emailService.SendEmailAsync(user.Email, "Confirm Email", confirmationLink);

            if (!emailResponse)
            {
                return BadRequest(InvalidConfirmEmailMessage);
            }

            return Created(new Uri("api/home/info", UriKind.Relative), null);

        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail([BindRequired] string email, [BindRequired] string token)
        {
            var isConfirmed = await _userService.ConfirmEmailAsync(email, token);

            if (isConfirmed.ServiceResultType == ServiceResultType.Success)
            {
                return Ok();
            }

            return BadRequest(isConfirmed.Message);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> Login([BindRequired] AuthUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            var loginResult = await _userService.TryLoginAsync(user);

            if (loginResult.ServiceResultType != ServiceResultType.Success)
            {
                return Unauthorized();
            }

            return Ok(loginResult.Result);
        }

    }
}
