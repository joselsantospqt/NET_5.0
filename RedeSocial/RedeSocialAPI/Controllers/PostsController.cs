using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static System.String;
using static System.Guid;
using System.Linq;
using Domain.Service;
using Domain.Entidade;
using Microsoft.AspNetCore.Authorization;
using Domain.Entidade.View;

namespace RedeSocialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private PostService _ServicePost;
        private CommentService _ServiceComment;
        private PessoaService _ServicePessoa;

        public PostsController(PostService servicePost, CommentService serviceComment, PessoaService servicePessoa)
        {
            _ServicePost = servicePost;
            _ServiceComment = serviceComment;
            _ServicePessoa = servicePessoa;
        }

        [HttpGet("getAllFeed")]
        public ActionResult GetAllFeed()
        {
            var todosPosts = _ServicePost.GetAll();
            IList<ItemPost> ListaDePost = new List<ItemPost>();

            GerarPostsFeed(todosPosts, ListaDePost);

            return Ok(ListaDePost.OrderByDescending(x => x.CreatedAt));
        }

        private void GerarPostsFeed(IEnumerable<Post> todosPosts, IList<ItemPost> ListaDePost)
        {
            foreach (var item in todosPosts)
            {
                ItemPost itemPost = new();
                Pessoa objPessoaPost = _ServicePessoa.GetPessoa(item.Autor.PessoaId);
                itemPost.IdPost = item.Id;
                itemPost.IdPessoaPost = item.Autor.PessoaId;
                itemPost.NomePessoaPost = objPessoaPost.Nome;
                itemPost.UrlImagemPerfilPessaPost = objPessoaPost.ImagemUrlPessoa;
                itemPost.ImagemUrlPost = item.ImagemUrlPost;
                itemPost.Message = item.Message;
                itemPost.CreatedAt = item.CreatedAt;
                itemPost.comments = new List<ItemComment>();
                foreach (var subItem in item.Comments)
                {
                    ItemComment itemComment = new();
                    Comment comment = _ServiceComment.GetComment(subItem.CommentId);
                    Pessoa objPessoaComment = _ServicePessoa.GetPessoa(comment.Pessoa.PessoaId);
                    itemComment.IdComment = comment.Id;
                    itemComment.IdPessoaComment = comment.Pessoa.PessoaId;
                    itemComment.NomePessoaComment = objPessoaComment.Nome;
                    itemComment.UrlImagemPessoaComment = objPessoaComment.ImagemUrlPessoa;
                    itemComment.Text = comment.Text;
                    itemComment.CreatedAt = comment.CreatedAt;
                    itemPost.comments.Add(itemComment);
                }
                ListaDePost.Add(itemPost);
            }
            //EU SEI QUE OQUE VOCÊ QUERIA VER ERA ALGO PARECIDO COM ISSO AQUI EM BAIXO, MAS O PRAZO E TEMPO NÃO DEU !
            //var query = from posts in todosPosts
            //            join comments in ListaComments on posts.Id equals comments.Post.PostId
            //            select new { idPessoaPost = posts.Autor.PessoaId, idPost = posts.Id, postMensagem = posts.Message, IdComment = comments.Id, commentsMessage = comments.Text, IdPessoaComment = comments.Pessoa.Id };
        }

        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            var todosPosts = _ServicePost.GetAll();

            return Ok(todosPosts);
        }



        [HttpGet("{id:Guid}")]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var post = _ServicePost.GetPost(id);

            if (post == null)
                return NoContent();

            return Ok(post);
        }

        [HttpPost("{id:Guid}")]
        public ActionResult Post([FromRoute] Guid id, [FromBody] CreatePost create)
        {

            var post = _ServicePost.CreatePost(id, create.Message, create.ImagemUrl);

            return Created("api/[controller]", post);
        }


        [HttpDelete("{id:Guid}")]
        public ActionResult Delete(Guid id)
        {

            _ServicePost.DeletePost(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        public ActionResult Put([FromRoute] Guid id, [FromBody] CreatePost update)
        {

            var updatePost = _ServicePost.UpdatePost(id, update.Message, update.ImagemUrl);

            return Ok(updatePost);

        }

        [HttpGet("getTodosPosts/{id:Guid}")]
        public ActionResult TodosPosts([FromRoute] Guid id)
        {
            var pessoa = _ServicePessoa.GetPessoa(id);
            var listaPosts = new List<Post>();

            foreach (var item in pessoa.Posts)
            {
                var post = _ServicePost.GetPost(item.PostId);
                listaPosts.Add(post);
            }
            return Ok(listaPosts);
        }

    }
}
