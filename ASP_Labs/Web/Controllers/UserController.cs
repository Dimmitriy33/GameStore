using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.BLL.Interfaces;
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
        public async Task<IActionResult> UserWithoutPasswordAsync(ApplicationUser user)
        {
            var UpdatedUser = _userService.UserWithoutPassword(user);

            return Ok(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(ApplicationUser user, string newPassword)
        {
            var IsChanged = _userService.ChangePassword(user.Email, newPassword);

            if(IsChanged.Result == true)
                return Ok(user);

            return BadRequest();
        }
    }
}
