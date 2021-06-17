using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;

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
        public async Task<IActionResult> UpdateAsync(ApplicationUser user)
        {
            var UpdatedUser = await _userService.UpdateUser(user);

            if(UpdatedUser.ServiceResultType == ServiceResultType.Error)
            {
                return BadRequest();
            }

            return Ok(UpdatedUser);
        }

        [HttpPost("password")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(ApplicationUser user, string newPassword)
        {
            var IsChanged = await _userService.ChangePassword(user, newPassword);

            if(IsChanged.ServiceResultType == ServiceResultType.Error)
            {
                return BadRequest(IsChanged.Message);
            }
            
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser(ApplicationUser user)
        {
            var findedUser = await _userService.FindUser(user);

            if (findedUser.ServiceResultType == ServiceResultType.Error)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
