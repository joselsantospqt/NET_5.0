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
        private ICommentRepositorio RepositorioComment { get; }

        public CommentService(ICommentRepositorio repositorioComment)
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

        public Comment CreateComment(Guid postId, string text, Guid pessoaId)
        {
            var comment = new Comment();
            comment.Text = text;
            comment.AddPost(postId);
            comment.AddPessoa(pessoaId);
            comment.CreatedAt =  DateTime.UtcNow;
            RepositorioComment.SaveUpdate(comment);

            return comment;
        }

        public Comment UpdateComment(Guid id, string text)
        {

            var comment = RepositorioComment.GetById(id);
            comment.Text = text;
            comment.UpDateTime = DateTime.UtcNow;
            RepositorioComment.SaveUpdate(comment);

            return comment;
        }


        public void DeleteComment(Guid id)
        {
            RepositorioComment.Remove(id);
        }

    }
}
