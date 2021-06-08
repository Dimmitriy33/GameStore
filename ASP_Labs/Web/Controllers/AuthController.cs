using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Web.Models;

namespace WebApp.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Email = model.Email,
                };

                var tryRegister = await _userManager.CreateAsync(user, model.Password);
                
                if(tryRegister.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("info", "Home");

                }
                else
                {
                    return BadRequest();
                }

            }

            return View(model);
        }




        public IActionResult Index()
        {
            return View();
        }
    }
}
