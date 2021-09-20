using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RedeSocialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private PessoaService _Service;

        public PessoasController(PessoaService service)
        {
            _Service = service;
        }

        [Authorize]
        [HttpGet("getAll")]
        [Authorize]
        public ActionResult GetAll()
        {
            var getAllPessoa = _Service.GetAll();

            return Ok(getAllPessoa);
        }


        [Authorize]
        [HttpGet("{id:Guid}")]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var pessoa = _Service.GetPessoa(id);

            if (pessoa == null)
                return NoContent();

            return Ok(pessoa);
        }


        //[HttpGet("{email}")]
        //public ActionResult GetByEmail([FromRoute] string email)
        //{

        //    var pessoa = _Service.GetPesoaEmail(email);

        //    if (pessoa == null)
        //        return NoContent();

        //    return Ok(pessoa);
        //}



        [HttpPost]
        public ActionResult Pessoa([FromBody] CreatePessoa create)
        {

            var pessoa = _Service.CreatePessoa(create.Id, create.Nome, create.Sobrenome, create.DataNascimento, create.Email, create.Senha);

            return Created("api/[controller]", pessoa);
        }

        [Authorize]
        [HttpDelete("{id:Guid}")]
        public ActionResult Delete(Guid id)
        {

            _Service.DeletePessoa(id);

            return NoContent();
        }

        [Authorize]
        [HttpPut("{id:Guid}")]
        public ActionResult Put([FromRoute] Guid id, [FromBody] CreatePessoa update)
        {

            var updatePessoa = _Service.UpdatePessoa(id, update.Nome, update.Sobrenome, update.DataNascimento, update.Email, update.Senha, update.UrlImagem);

            return Ok(updatePessoa);

        }

        [Authorize]
        [HttpPost("{idPessoa:Guid}/{idAmigo:Guid}")]
        public ActionResult CadastraAmigo([FromRoute] Guid idPessoa, [FromRoute] Guid idAmigo)
        {
            var pessoa = _Service.CadastraAmigo(idPessoa, idAmigo);
            return Ok(pessoa);
        }
    }
}
