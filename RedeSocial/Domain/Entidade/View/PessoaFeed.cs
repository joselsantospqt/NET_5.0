using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.View
{
    public class PessoaFeed
    {
        public Pessoa pessoa { get; set; }
        public List<Post> Posts { get; set; }
    }
}
