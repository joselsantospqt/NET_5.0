using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Trilha
    {
        public Trilha()
        {
            AutorTrilha = new PessoaTrilha();
        }

        [Key]
        public Guid Id { get; set; }

        public string NomeTrilha { get; set; }

        public string ImagemTrilha { get; set; }

        [NotMapped]
        public PessoaTrilha AutorTrilha { get; set; }

        public string Local { get; set; }

        public DateTime DataTrilha { get; set; }

        public string DuracaoTrilha { get; set; }

        public string Nivel { get; set; }

        public string Descricao { get; set; }

        public IList<PessoaTrilha> Trilhas { get; set; }

        internal void AddPessoaTrilha(Guid pessoaId)
        {
            AutorTrilha = new PessoaTrilha() { PessoaId = pessoaId, TrilhaId = this.Id };
        }
    }
}
