using System;
using System.Linq;
using System.Threading.Tasks;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Web_Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LoginController : ControllerBase
	{
		private KonferencijaContext Context { get; set; }
		public IConfiguration _configuration;
		public LoginController(KonferencijaContext context, IConfiguration config)
		{
			this.Context = context;
			this._configuration = config;
		}

		[HttpPost]
		[Route("LogIn")]
		public async Task<IActionResult> LogIn([FromBody]UserInfo userInfo)
		{
			Tokeni t=new Tokeni();
			if (userInfo == null || string.IsNullOrEmpty(userInfo.Email)
				|| string.IsNullOrEmpty(userInfo.Password))
                return BadRequest("Nevalidni podaci!");

			var predavac = await this.Context.Predavaci
				.Where(x => x.Email == userInfo.Email && x.Lozinka == userInfo.Password).FirstOrDefaultAsync();

			var claims = new List<Claim>();

            if (predavac != null)
            {
				if(predavac.ArchiveFlag==1)
				{
					predavac.ArchiveFlag=0;
					await Context.SaveChangesAsync();
				}
                claims.Add(new Claim("Id", predavac.ID.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, predavac.Ime));
                claims.Add(new Claim(ClaimTypes.Email, predavac.Email));
				claims.Add(new Claim(ClaimTypes.Role, "Predavac"));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: signIn);
				t.Token=new JwtSecurityTokenHandler().WriteToken(token);
				t.Role="Predavac";
				t.ID=predavac.ID;

				return Ok(t);
			}

            var slusalac = await this.Context.Slusaoci
				.Where(x => x.Email == userInfo.Email && x.Lozinka == userInfo.Password).FirstOrDefaultAsync();
		
			if (slusalac!= null)
			{
				if(slusalac.ArchiveFlag==1)
				{
					slusalac.ArchiveFlag=0;
					await Context.SaveChangesAsync();
				}
				claims.Add(new Claim("Id", slusalac.ID.ToString()));
				claims.Add(new Claim(ClaimTypes.Name, slusalac.Ime));
				claims.Add(new Claim(ClaimTypes.Email, slusalac.Email));
				claims.Add(new Claim(ClaimTypes.Role, "Slusalac"));

				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
				var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
				var token = new JwtSecurityToken(
					_configuration["Jwt:Issuer"],
					_configuration["Jwt:Audience"],
					claims,
					expires: DateTime.UtcNow.AddHours(1),
					signingCredentials: signIn);				

				t.Token=new JwtSecurityTokenHandler().WriteToken(token);
				t.Role="Slusalac";
				t.ID=slusalac.ID;

				return Ok(t);
			}

			var organizator = await this.Context.Organizator
				.Where(x => x.Email == userInfo.Email && x.Lozinka == userInfo.Password).FirstOrDefaultAsync();

			if (organizator != null)
			{
				claims.Add(new Claim("Id", organizator.ID.ToString()));
				claims.Add(new Claim(ClaimTypes.Name, organizator.Ime));
				claims.Add(new Claim(ClaimTypes.Email, organizator.Email));
				claims.Add(new Claim(ClaimTypes.Role, "Organizator"));

				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
				var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
				var token = new JwtSecurityToken(
					_configuration["Jwt:Issuer"],
					_configuration["Jwt:Audience"],
					claims,
					expires: DateTime.UtcNow.AddHours(1),
					signingCredentials: signIn);

				t.Token=new JwtSecurityTokenHandler().WriteToken(token);
				t.Role="Organizator";
				t.ID=organizator.ID;

				return Ok(t);
			}

			return NotFound("Korisnik ne postoji!");
		}
	}
}
