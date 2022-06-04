using CollaborativeWhiteboard.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CollaborativeWhiteboard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly JwtFeatures.JwtHandler jwtHandler;

        public AccountController(UserManager<IdentityUser> userManager, JwtFeatures.JwtHandler jwtHandler)
        {
            this.userManager = userManager;
            this.jwtHandler = jwtHandler;
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
            if (userExists == null || !await userManager.CheckPasswordAsync(userExists, model.Password))
            {
                return Unauthorized(new LoginResponse { IsSuccessful = false, ErrorMessage = "Invalid Authentication" });
            }

            var signingCredentials = jwtHandler.GetSigningCredentials();
            var claims = jwtHandler.GetClaims(userExists);
            var tokenOptions = jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new LoginResponse
            {
                IsSuccessful = true,
                Token = token,
            });
        }

        /*[HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }*/
    }
}
