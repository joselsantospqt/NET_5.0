using Domain.Service;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrilhasController : ControllerBase
    {
        private TrilhaService _Service;

        public TrilhasController(TrilhaService service)
        {
            _Service = service;
        }


        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            var todasTrilhas = _Service.GetAll();

            return Ok(todasTrilhas);
        }



        [HttpGet("{id:Guid}")]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var trilha = _Service.GetPost(id);

            if (trilha == null)
                return NoContent();

            return Ok(trilha);
        }

        [HttpPost("")]
        public ActionResult Post(
            [FromServices] BancoDeDados bancoDeDados,
            [FromBody] Domain.Entidade.Trilha trilha)
        {

            var trilhaRegister = _Service.CreateTrilha(trilha);

            return Created("api/[controller]", trilhaRegister);
        }


        [HttpDelete("{id:Guid}")]
        public ActionResult Delete(Guid id)
        {
            _Service.DeleteTrilha(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        public ActionResult Put([FromRoute] Guid id, [FromServices] BancoDeDados bancoDeDados, [FromBody] Domain.Entidade.Trilha trilha)
        {

            var updateTrilha = _Service.UpdateTrilha(trilha.Id,
                trilha.NomeTrilha,
                trilha.ImagemTrilha,
                trilha.DuracaoTrilha,
                trilha.Local,
                trilha.Nivel,
                trilha.Descricao);

            return Ok(updateTrilha);

        }
    }
}
