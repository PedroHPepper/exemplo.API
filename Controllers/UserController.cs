using example.API.Filters;
using example.API.Models;
using example.API.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace example.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [SwaggerResponse(statusCode:200, description: "Sucesso ao autenticar", type: typeof(RegisterViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos Obrigatórios", type: typeof(FieldValidatorViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro no servidor", type: typeof(GenericErrorViewMode))]
        [HttpPost("login")]
        [CustomModelStateValidation]
        public IActionResult Login(LoginViewModelInput loginViewModelInput)
        {
            var UserViewModelOutput = new UserViewModelOutput
            {
                Code = 1,
                Email = "peterhanz@gmail.com",
                Login = "pepper"
            };

            var secret = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfigurations:Secret").Value);
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, UserViewModelOutput.Code.ToString()),
                    new Claim(ClaimTypes.Name, UserViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, UserViewModelOutput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token =  jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new { 
                Token = token,
                User = UserViewModelOutput
            });
        }


        [HttpPost("registrar")]
        [CustomModelStateValidation]
        public IActionResult Register(RegisterViewModelInput registerViewModelInput)
        {
            return Created("", registerViewModelInput);
        }
    }
}
