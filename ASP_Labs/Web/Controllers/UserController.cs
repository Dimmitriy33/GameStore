using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;

namespace WebApp.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        #region Services

        private readonly IUserService _userService;
        private readonly IClaimsReader _claimsHelper;

        #endregion

        public UserController(IUserService userService, IClaimsReader claimsHelper)
        {
            _userService = userService;
            _claimsHelper = claimsHelper;
        }

        [HttpPut]
        public async Task<IActionResult> Update([BindRequired] UserDTO user)
        {
            var updatedUser = await _userService.UpdateUserInfoAsync(user);

            if (updatedUser.ServiceResultType is not ServiceResultType.Success)
            {
                return BadRequest();
            }

            return Ok(updatedUser);
        }

        [HttpPatch("password")]
        public async Task<IActionResult> ChangePassword([BindRequired, FromBody] JsonPatchDocument<ResetPasswordUserDTO> patch)
        {
            var user = new ResetPasswordUserDTO();
            patch.ApplyTo(user);

            var IsChanged = await _userService.ChangePasswordAsync(user);

            if (IsChanged.ServiceResultType is not ServiceResultType.Success)
            {
                return BadRequest(IsChanged.Message);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var result = _claimsHelper.GetUserId(User);
            if (result.ServiceResultType is not ServiceResultType.Success)
            {
                return StatusCode((int)result.ServiceResultType);
            }

            var foundUser = await _userService.FindUserByIdAsync(result.Result);

            if (foundUser.ServiceResultType is not ServiceResultType.Success)
            {
                return NotFound();
            }

            return Ok(foundUser.Result);
        }
    }
}
