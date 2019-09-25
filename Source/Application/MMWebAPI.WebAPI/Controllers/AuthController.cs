using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MMWebAPI.Application.InOut.User;
using MMWebAPI.Application.TokenConfiguration;
using System;
using System.IdentityModel.Tokens.Jwt;
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
                return Created("", GerarJwt());
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login (LoginUserRequest userRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _signInManager.PasswordSignInAsync(userRequest.Email, userRequest.Password,false,true);

            if(result.Succeeded)
                return Ok(GerarJwt());

            if (result.IsLockedOut)
                return Unauthorized("Usuário bloqueado por tentativas inválidas.");

            return BadRequest();
        }


        private string GerarJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            return tokenHandler.WriteToken(token);
        }
    }
}