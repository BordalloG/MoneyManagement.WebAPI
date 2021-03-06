﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MMWebAPI.Application.InOut.User;
using MMWebAPI.Application.TokenConfiguration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MMWebAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }
        [HttpPost("Registrar")]
        public async Task<ActionResult> Register (RegisterUserRequest userRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new IdentityUser()
            {
                UserName = userRequest.Email,
                Email = userRequest.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRequest.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Created("", await GerarJwt(user.Email));
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login (LoginUserRequest userRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _signInManager.PasswordSignInAsync(userRequest.Email, userRequest.Password,false,true);

            if(result.Succeeded)
                return Ok(await GerarJwt(userRequest.Email));

            if (result.IsLockedOut)
                return Unauthorized("Usuário bloqueado por tentativas inválidas.");

            return BadRequest();
        }


        private async Task<LoginResponse> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role",userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                Subject = identityClaims,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            var encodeToken =  tokenHandler.WriteToken(token);

            var response = new LoginResponse()
            {
                AccessToken = encodeToken,
                ExpiresIn =  TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserToken()
                {
                    Id = user.Id,

                    Email = user.Email,
                    Claims = claims.Select(x=> new ClaimResponse()
                    {
                        Type = x.Type,
                        Value = x.Value
                    })
                }
            };
            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}