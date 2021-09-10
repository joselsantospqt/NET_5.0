using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;
using static System.Guid;
using Domain.Repositorio;
using Domain.Entidade;

namespace Domain.Service
{
    public class PostService
    {
        private IPostRepositorio RepositorioPost { get; }

        public PostService(IPostRepositorio repositorioPost)
        {
            RepositorioPost = repositorioPost;
        }

        public IEnumerable<Post> GetAll()
        {
            return RepositorioPost.GetAll();
        }
        public Post GetPost(Guid id)
        {
            return RepositorioPost.GetById(id);
        }

        //IMPLEMENTAR MAIS PARA FRENTE
        //public Post GetAuthor(string author)
        //{
        //    if (IsNullOrWhiteSpace(author))
        //    {
        //        return db.Post.Find(author);
        //    }

        //    return db.Post.Where(x => x.Author == author).FirstOrDefault();

        //}

        public Post CreatePost(Guid pessoaId, string message, string imagemUrl)
        {

            var post = new Post();
            post.Message = message;
            post.ImagemUrl = imagemUrl;
            post.CreatedAt = DateTime.UtcNow;
            post.UpdatedAt = new DateTime();
            post.AddPessoa(pessoaId);
            RepositorioPost.SaveUpdate(post);

            return post;
        }

        public Post UpdatePost(Guid id, string message, string imagemUrl)
        {

            var post = RepositorioPost.GetById(id);
            post.Message = message;
            post.ImagemUrl = imagemUrl;
            post.UpdatedAt = DateTime.UtcNow;
            RepositorioPost.SaveUpdate(post);

            return post;
        }

   
        public void DeletePost(Guid id)
        {
            RepositorioPost.Remove(id);

        }

    }
}
