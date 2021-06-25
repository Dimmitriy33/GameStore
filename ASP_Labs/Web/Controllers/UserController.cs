using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Helpers;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;

namespace WebApp.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        public async Task<IActionResult> Update([BindRequired] UserDTO user)
        {
            var updatedUser = await _userService.UpdateUserInfoAsync(user);

            if (updatedUser.ServiceResultType != ServiceResultType.Success)
            {
                return BadRequest();
            }

            return Ok(updatedUser);
        }

        [HttpPatch("password")]
        public async Task<IActionResult> ChangePassword([BindRequired, FromBody] JsonPatchDocument patch)
        {
            var user = new ResetPasswordUserDTO();
            patch.ApplyTo(user);

            var IsChanged = await _userService.ChangePasswordAsync(user);

            if (IsChanged.ServiceResultType != ServiceResultType.Success)
            {
                return BadRequest(IsChanged.Message);
            }

            return Ok();
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUser()
        {
            var userId = ClaimsHelper.GetUserId(User);
            var foundUser = await _userService.FindUserByIdAsync(Guid.Parse(userId));

            if (foundUser.ServiceResultType != ServiceResultType.Success)
            {
                return NotFound();
            }

            return Ok(foundUser);
        }
    }
}
