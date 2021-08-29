using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidade
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid PessoaId { get; set; } 
        public string Text{ get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpDateTime { get; set; }
    }
}
