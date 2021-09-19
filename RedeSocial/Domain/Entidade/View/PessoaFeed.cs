using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.View
{
    public class PessoaFeed
    {
        public Post Posts { get; set; }
        public Pessoa AutorPost { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
