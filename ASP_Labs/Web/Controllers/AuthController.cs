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
        #region Services

        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        #endregion

        #region Constants

        private static string InvalidRegisterMessage = "Invalid Register Attempt";
        private static string InvalidConfirmEmailMessage = "Invalid Confirm Email Attempt";

        #endregion

        public AuthController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        /// <summary>
        /// Create user (sign up)
        /// </summary>
        /// <param name="user">AuthenticationUser model</param>
        /// <response code="201">Successful customer registration</response>
        /// <response code="400">Failed customer registration</response>
        [HttpPost("sign-up")]
        public async Task<ActionResult<string>> Register([BindRequired] AuthUserDTO user)
        {
            var registerStatus = await _userService.TryRegisterAsync(user);

            if (registerStatus.ServiceResultType is not ServiceResultType.Success)
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

        /// <summary>
        /// Confirm user email (confirm email)
        /// </summary>
        /// <param name=email">User email</param>
        /// <param name="token">User access token</param>
        /// <response code="200">Successful user email confirmation</response>
        /// <response code="400">Failed user email confirmation</response>
        [HttpGet("confirm")]
        public async Task<ActionResult<string>> ConfirmEmail([BindRequired] string email, [BindRequired] string token)
        {
            var isConfirmed = await _userService.ConfirmEmailAsync(email, token);

            return StatusCode((int)isConfirmed.ServiceResultType, isConfirmed.Message);
        }

        /// <summary>
        /// Authenticate user (sign in)
        /// </summary>
        /// <param name="user">AuthenticationUser model</param>
        /// <response code="200">Successful authentication</response>
        /// <response code="401">Failed authentication</response>
        [HttpPost("sign-in")]
        public async Task<ActionResult<string>> Login([BindRequired] AuthUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            var loginResult = await _userService.TryLoginAsync(user);

            return StatusCode((int)loginResult.ServiceResultType, loginResult.Message);
        }
    }
}
