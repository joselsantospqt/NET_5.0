using Domain.Entidade;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RedeSocialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private CommentService _Service { get; }

        public CommentsController(CommentService service)
        {
            _Service = service;
        }

        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            var getAllComment = _Service.GetAll();

            return Ok(getAllComment);
        }



        [HttpGet("{id:Guid}")]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var pessoa = _Service.GetComment(id);

            if (pessoa == null)
                return NoContent();

            return Ok(pessoa);
        }

        [HttpPost]
        public ActionResult Pessoa([FromBody] Comment create)
        {

            var comment = _Service.CreateComment(create.PostId, create.PessoaId, create.Text);

            return Created("api/[controller]", comment);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {

            _Service.DeleteComment(id);

            return NoContent();
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, Comment comment)
        {

            var updateComment = _Service.UpdateComment(id, comment.Text);

            return Ok(updateComment);

        }
    }
}
