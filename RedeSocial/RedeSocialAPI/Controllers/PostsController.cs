using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static System.String;
using static System.Guid;
using System.Linq;
using Domain.Service;
using Domain.Entidade;

namespace RedeSocialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private PostService _Service;

        public PostsController(PostService serivce)
        {
            _Service = serivce;
        }

        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            var todosPosts = _Service.GetAll();

            return Ok(todosPosts);
        }



        [HttpGet("{id:Guid}")]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var post = _Service.GetPost(id);

            if (post == null)
                return NoContent();

            return Ok(post);
        }

        [HttpPost("{id:Guid}")]
        public ActionResult Post([FromRoute] Guid id, [FromBody] CreatePost create)
        {

            var post = _Service.CreatePost(id, create.Message, create.ImagemUrl);

            return Created("api/[controller]", post);
        }


        [HttpDelete("{id:Guid}")]
        public ActionResult Delete(Guid id)
        {

            _Service.DeletePost(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        public ActionResult Put([FromRoute] Guid id, [FromBody] CreatePost update)
        {

            var updatePost = _Service.UpdatePost(id, update.Message, update.ImagemUrl);

            return Ok(updatePost);

        }

    }
}
