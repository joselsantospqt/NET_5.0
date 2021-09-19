using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class PessoaAmigos
    {
        public int Id { get; set; }
        public Guid PessoaId { get; set; }
        public Guid AmigoId { get; set; }
    }
}
