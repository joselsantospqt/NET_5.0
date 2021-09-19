using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Pessoa
    {
        public Pessoa() { Posts = new List<PessoaPost>(); Amigos = new List<PessoaAmigos>(); }
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ImagemUrlPessoa { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IList<PessoaPost> Posts { get; set; }
        public IList<PessoaAmigos> Amigos { get; set; }

        internal void AddAmigo(Guid amigoId)
        {
            Amigos.Add(new PessoaAmigos() { AmigoId = amigoId, PessoaId = this.Id });
        }
    }
}
