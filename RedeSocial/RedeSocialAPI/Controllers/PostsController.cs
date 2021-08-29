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



        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var post = _Service.GetPost(id);

            if (post == null)
                return NoContent();

            return Ok(post);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Post create)
        {

            var post = _Service.CreatePost(create.Author, create.CreatedAt, create.Subject);

            return Created("api/[controller]", post);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {

            _Service.DeletePost(id);

            return NoContent();
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, Post update)
        {

            var updatePost = _Service.UpdatePost(id, update.Subject);

            return Ok(updatePost);

        }

    }
}
