using System;

namespace Domain.Entidade
{
    public class CommentPessoa
    {
        public int Id { get; set; }
        public Guid PessoaId { get; set; }
        public Guid CommentId { get; set; }
    }
}