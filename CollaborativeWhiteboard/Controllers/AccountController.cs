using CollaborativeWhiteboard.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollaborativeWhiteboard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if(userExists != null)
            {
                return ValidationProblem(modelStateDictionary: ModelState, statusCode:400);
            }

            var user = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if(!result.Succeeded)
            {
                return ValidationProblem(modelStateDictionary: ModelState, statusCode: 400);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return ValidationProblem(modelStateDictionary: ModelState, statusCode: 400);
            }

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if(!result.Succeeded)
            {
                return ValidationProblem(modelStateDictionary: ModelState, statusCode: 400);
            }

            return Ok(new LoginResult
            {
                Id = userExists.Id,
                Email = userExists.Email,
            });
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
    }
}
