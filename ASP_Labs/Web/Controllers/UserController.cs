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
        private readonly IClaimsReader _claimsReader;

        #endregion

        public UserController(IUserService userService, IClaimsReader claimsReader)
        {
            _userService = userService;
            _claimsReader = claimsReader;
        }

        /// <summary>
        /// Update user information
        /// </summary>
        /// /// <param name="user">User for update</param>
        /// <response code="200">Successful update</response>
        /// <response code="400">Failed update</response>
        [HttpPut]
        public async Task<ActionResult<UserDTO>> Update([BindRequired] UserDTO user)
        {
            var updatedUser = await _userService.UpdateUserInfoAsync(user);

            return StatusCode((int)updatedUser.ServiceResultType, updatedUser.Result);
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <response code="200">Password successfully changed</response>
        /// <response code="400">Failed password change</response>
        [HttpPatch("password")]
        public async Task<ActionResult<string>> ChangePassword([BindRequired, FromBody] JsonPatchDocument<ResetPasswordUserDTO> patch)
        {
            var user = new ResetPasswordUserDTO();
            patch.ApplyTo(user);

            var isChanged = await _userService.ChangePasswordAsync(user);

            return StatusCode((int)isChanged.ServiceResultType, isChanged.Message);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <response code="200">Found user successfully</response>
        /// <response code="400">Failed to find user</response>
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetUser()
        {
            var result = _claimsReader.GetUserId(User);

            if (result.ServiceResultType is not ServiceResultType.Success)
            {
                return StatusCode((int)result.ServiceResultType);
            }

            var foundUser = await _userService.FindUserByIdAsync(result.Result);

            return StatusCode((int)foundUser.ServiceResultType, foundUser.Result);
        }
    }
}
