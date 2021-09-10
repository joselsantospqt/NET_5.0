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

        [HttpPost("{id:Guid}")]
        public ActionResult Post([FromRoute] Guid id, [FromBody] CreateComment create)
        {

            var comment = _Service.CreateComment(id, create.Text);

            return Created("api/[controller]", comment);
        }


        [HttpDelete("{id:Guid}")]
        public ActionResult Delete(Guid id)
        {

            _Service.DeleteComment(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        public ActionResult Put([FromRoute] Guid id, [FromBody] CreateComment create)
        {

            var updateComment = _Service.UpdateComment(id, create.Text);

            return Ok(updateComment);

        }
    }
}
