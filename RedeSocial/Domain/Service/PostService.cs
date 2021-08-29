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
        private IRepositorioPost RepositorioPost { get; }

        public PostService(IRepositorioPost repositorioPost)
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

        public Post CreatePost(string pAuthor, DateTime pCreatedAt, string pSubject)
        {

            var post = new Post();
            post.Id = NewGuid();
            post.Author = pAuthor;
            post.CreatedAt = pCreatedAt;
            post.Subject = pSubject;
            RepositorioPost.Save(post);

            return post;
        }

        public Post UpdatePost(Guid id, string pSubject)
        {

            var post = RepositorioPost.GetById(id);
            post.UpdatedAt = DateTime.UtcNow;
            post.Subject = pSubject;
            RepositorioPost.Update(post);

            return post;
        }

   
        public void DeletePost(Guid id)
        {
            RepositorioPost.Remove(id);

        }

    }
}
