using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([BindRequired] UserDTO user)
        {
            var UpdatedUser = await _userService.UpdateUserInfoAsync(user);

            if (UpdatedUser.ServiceResultType == ServiceResultType.Error)
            {
                return BadRequest();
            }

            return Ok(UpdatedUser);
        }

        [HttpPatch("password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([BindRequired] JsonPatchDocument patch)
        {

            var IsChanged = await _userService.ChangePasswordAsync(patch);

            if (IsChanged.ServiceResultType == ServiceResultType.Error)
            {
                return BadRequest(IsChanged.Message);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser([BindRequired] Guid id)
        {
            var findedUser = await _userService.FindUserByIdAsync(id);

            if (findedUser.ServiceResultType == ServiceResultType.Error)
            {
                return NotFound();
            }

            return Ok(findedUser);
        }
    }
}
