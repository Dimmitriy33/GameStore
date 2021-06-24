using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        public async Task<IActionResult> ChangePassword([BindRequired][FromBody] JsonPatchDocument patch)
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
        public async Task<IActionResult> GetUser()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
            var findedUser = await _userService.FindUserByIdAsync(Guid.Parse(userId));

            if (findedUser.ServiceResultType == ServiceResultType.Error)
            {
                return NotFound();
            }

            return Ok(findedUser);
        }
    }
}
