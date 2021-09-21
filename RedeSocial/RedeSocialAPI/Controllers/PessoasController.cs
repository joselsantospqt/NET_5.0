using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

    
        [HttpGet("getAll")]
        [Authorize]
        public ActionResult GetAll()
        {
            var getAllPessoa = _Service.GetAll();

            return Ok(getAllPessoa);
        }


    
        [HttpGet("{id:Guid}")]
        [Authorize]
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

    
        [HttpDelete("{id:Guid}")]
        [Authorize]
        public ActionResult Delete(Guid id)
        {

            _Service.DeletePessoa(id);

            return NoContent();
        }

    
        [HttpPut("{id:Guid}")]
        [Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Pessoa update)
        {
            Pessoa pessoaUpdate = update;
            pessoaUpdate.Id = id;
            var updatePessoa = _Service.UpdatePessoa(pessoaUpdate);

            return Ok(updatePessoa);

        }

    
        [HttpPost("cadastrarAmigo/{idPessoa:Guid}")]
        [Authorize]
        public ActionResult CadastraAmigo([FromRoute] Guid idPessoa, [FromBody] CreateAmigo idAmigo)
        {
            var pessoa = _Service.CadastraAmigo(idPessoa, idAmigo.Id);
            return Ok(pessoa);
        }

    
        [HttpGet("getTodosAmigos/{id:Guid}")]
        [Authorize]
        public ActionResult todosAmigos([FromRoute] Guid id)
        {
            var pessoa = _Service.GetPessoa(id);
            var listaAmigos = new List<Pessoa>();

            foreach (var item in pessoa.Amigos)
            {
                var amigo = _Service.GetPessoa(item.AmigoId);
                listaAmigos.Add(amigo);
            }
            return Ok(listaAmigos);
        }

        [HttpPut("removerAmigo/{idPessoa:Guid}")]
        [Authorize]
        public ActionResult removerAmigosById([FromRoute] Guid idPessoa, [FromBody] CreateAmigo idAmigo)
        {
            var pessoa = _Service.GetPessoa(idPessoa);
            PessoaAmigos amigo = new();

            foreach (var item in pessoa.Amigos)
            {
                if (item.AmigoId == idAmigo.Id)
                {
                    amigo = item;
                }
            }
            pessoa.Amigos.Remove(amigo);
            _Service.UpdatePessoa(pessoa);
            return Ok(pessoa);
        }
    }
}
