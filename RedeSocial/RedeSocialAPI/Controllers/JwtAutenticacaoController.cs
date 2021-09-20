using Domain.Entidade.Request;
using Domain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAutenticacaoController : ControllerBase
    {
        private IConfiguration _config;
        private PessoaService _Service;
        public JwtAutenticacaoController(IConfiguration Configuration, PessoaService pessoaService)
        {
            _config = Configuration;
            _Service = pessoaService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] CredenciaisUsuario Login)
        {
            bool resultado = ValidarUsuario(Login);
            if (resultado)
            {
                var tokenString = GerarTokenJWT();
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
        private string GerarTokenJWT()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: expiry, signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        private bool ValidarUsuario(CredenciaisUsuario Login)
        {
            var pessoa = _Service.GetPessoa(Login.idUsuario);

            if (Login.idUsuario== pessoa.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
