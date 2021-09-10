using System;

namespace Domain.Entidade
{
    public class PessoaPost
    {
        public int Id { get; set; }
        public Guid PessoaId { get; set; }
        public Guid PostId { get; set; }
    }
}