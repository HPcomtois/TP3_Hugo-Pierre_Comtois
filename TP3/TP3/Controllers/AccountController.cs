﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TP3.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TP3.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AccountController : ControllerBase
	{

		UserManager<User> userManager;
		IConfiguration configuration;


		public AccountController(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
			this.configuration = configuration;
        }

		//POST api/Account/Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO login)
		{
			User? user = await userManager.FindByNameAsync(login.UserName!);
			if(user != null && await userManager.CheckPasswordAsync(user, login.Password!))
			{
				IList<string> roles = await userManager.GetRolesAsync(user);

				List<Claim> authClaims = new List<Claim>();

                foreach (string role in roles)
                {
					authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

				authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

				SymmetricSecurityKey authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

				JwtSecurityToken token = new JwtSecurityToken(
					issuer: "http://localhost:5042",
					audience: "http://localhost:4200",
					claims: authClaims,
					expires: DateTime.Now.AddHours(1),
					signingCredentials: new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256) 
                    );

                return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token),
					validTo = token.ValidTo
				});
            }
			return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "L'utilisateur est introuvable ou le mot de passe ne concorde pas." });
		}

        //POST api/Account/Register
        [HttpPost]
		public async Task<ActionResult> Register(RegisterDTO register)
		{
			if(register.Password != register.PasswordConfirm)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, 
					new { Error = "Le mot de passe et la confirmation ne sont pas identique." });
            }

			User user = new User()
			{
				UserName = register.UserName,
				Email = register.Email,
			};
			IdentityResult identityResult = await this.userManager.CreateAsync(user, register.Password!);

			if(!identityResult.Succeeded)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Error = identityResult.Errors });
            }

			return Ok();
		}

		
	}
}
