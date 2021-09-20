using Domain.Entidade;
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
using System.Security.Claims;
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
                var pessoa = _Service.GetPessoa(Login.idUsuario);
                var tokenString = GerarTokenJWT(pessoa);
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
        private string GerarTokenJWT(Pessoa pessoa)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>();

            claims.Add(new Claim("id", pessoa.Id.ToString()));

            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"])), SecurityAlgorithms.HmacSha256),
                Audience = _config["Jwt:Audience"],
                Issuer = _config["Jwt:Issuer"]
            };

            var securityToken = tokenHandler.CreateToken(token);
            var stringToken = tokenHandler.WriteToken(securityToken);
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
