using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade.Request
{
    public class CreateTrilha
    {
        public string NomeTrilha { get; set; }

        public string ImagemTrilha { get; set; }

        public string Local { get; set; }

        public DateTime DataTrilha { get; set; }

        public string DuracaoTrilha { get; set; }

        public string Nivel { get; set; }

        public string Descricao { get; set; }
    }
}
