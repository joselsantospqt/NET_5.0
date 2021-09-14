using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidade
{
    public class Comment
    {
        public Comment(){Post = new PostComment(); Pessoa = new CommentPessoa(); }
        [Key]
        public Guid Id { get; set; }
        public string Text{ get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpDateTime { get; set; }
        public PostComment Post { get; set; }
        public CommentPessoa Pessoa { get; set; }

        internal void AddPost(Guid postId)
        {
            Post = new PostComment() { PostId = postId, CommentId = this.Id };
        }

        internal void AddPessoa(Guid pessoaId)
        {
            Pessoa = new CommentPessoa() { PessoaId = pessoaId, CommentId = this.Id };
        }
    }
}
