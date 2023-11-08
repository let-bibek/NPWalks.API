using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NPWalks.API.Models.DTO;

namespace NPWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;

        }


        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.UserPassword);
            if (identityResult.Succeeded)
            {
                // Add role to the user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                }
                if (identityResult.Succeeded)
                {
                    return Ok("User Registration is Successful. Please Proceed with Login.");
                }

            }

            return BadRequest("The User Registration Failed.");
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            // Get the user
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user != null)
            {
                var passCheck = await userManager.CheckPasswordAsync(user, loginRequestDto.UserPassword);

                if (passCheck)
                {
                    return Ok("Login Successful.");
                }
            }

            return BadRequest("Invalid Username or Password.");
        }
    }
}