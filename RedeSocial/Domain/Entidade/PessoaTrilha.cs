using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entidade
{
    public class PessoaTrilha
    {
        [Key]
        public int Id { get; set; }
        public Guid PessoaId { get; set; }
        
        public Guid TrilhaId { get; set; }
    }
}
