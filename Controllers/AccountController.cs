using AspNetCoreTraining.Database.Entities;
using AspNetCoreTraining.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTraining.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUserEntity> _userManager;
        private readonly SignInManager<ApplicationUserEntity> _signInManager;

        public AccountController(UserManager<ApplicationUserEntity> userManager, SignInManager<ApplicationUserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Route("account/")]
        [HttpGet]
        [Route("getCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user==null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var newUser = new ApplicationUserEntity
            {
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
            };
            var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);
            if(result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                await _userManager.ConfirmEmailAsync(newUser, token);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var foundUser = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if(foundUser==null)
            {
                return NotFound();
            }
            var resault = await _signInManager.PasswordSignInAsync(foundUser, userLoginDto.Password, true, false);
            if(resault.Succeeded)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
