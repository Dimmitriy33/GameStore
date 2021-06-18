﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApp.BLL.Interfaces;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Services
{
	public class JwtGenerator : IJwtGenerator
	{
		private readonly SymmetricSecurityKey _key;
		private readonly UserManager<ApplicationUser> _userManager;

		public JwtGenerator(IConfiguration config, UserManager<ApplicationUser> userManager)
		{
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwtSettings:TokenKey"]));
			_userManager = userManager;
		}

		public string CreateToken(ApplicationUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.NameId, user.Id),
				new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
				new Claim(JwtRegisteredClaimNames.NameId, _userManager.GetRolesAsync(user).Result[0])
			};

			var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = credentials
			};
			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}