using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Post
    {
        public Post() { Comments = new List<PostComment>(); Autor = new PessoaPost(); }
        [Key]
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string ImagemUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public PessoaPost Autor { get; set; }
        public IList<PostComment> Comments { get; set; }

        internal void AddPessoa(Guid pessoaId)
        {
            Autor = new PessoaPost() { PessoaId = pessoaId, PostId = this.Id };
        }
    }
}
