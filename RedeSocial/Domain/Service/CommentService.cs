using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;
using static System.Guid;

namespace Domain.Service
{
    public class CommentService
    {
        private IRepositorioComment RepositorioComment { get; }

        public CommentService(IRepositorioComment repositorioComment)
        {
            RepositorioComment = repositorioComment;
        }

        public IEnumerable<Comment> GetAll()
        {
            return RepositorioComment.GetAll();
        }
        public Comment GetComment(Guid id)
        {
            return RepositorioComment.GetById(id);
        }

        public Comment CreateComment(Guid postId, Guid pessoaId, string text)
        {

            var Comment = new Comment();
            Comment.Id = NewGuid();
            Comment.PostId = postId;
            Comment.PessoaId = pessoaId;
            Comment.Text = text;
            Comment.CreatedAt =  DateTime.UtcNow;

            RepositorioComment.Save(Comment);

            return Comment;
        }

        public Comment UpdateComment(Guid id, string text)
        {

            var Comment = RepositorioComment.GetById(id);

            Comment.Text = text;
            Comment.UpDateTime = DateTime.UtcNow;

            RepositorioComment.Update(Comment);

            return Comment;
        }


        public void DeleteComment(Guid id)
        {
            RepositorioComment.Remove(id);
        }

    }
}
