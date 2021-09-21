using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.View
{
   public class ItemComment
    {
        public Guid IdComment { get; set; }
        public Guid IdPessoaComment { get; set; }
        public string NomePessoaComment { get; set; }
        public string UrlImagemPessoaComment { get; set; }

        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    

    }
}
