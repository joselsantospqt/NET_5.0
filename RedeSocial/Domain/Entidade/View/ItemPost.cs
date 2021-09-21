using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.View
{
    public class ItemPost
    {
        public Guid  IdPost { get; set; }
        public Guid  IdPessoaPost { get; set; }
        public string  NomePessoaPost { get; set; }
        public string Message { get; set; }
        public string ImagemUrlPost { get; set; }
        public string UrlImagemPerfilPessaPost { get; set; }

        public DateTime CreatedAt { get; set; }
        public IList<ItemComment> comments { get; set; }
        
    }
}
