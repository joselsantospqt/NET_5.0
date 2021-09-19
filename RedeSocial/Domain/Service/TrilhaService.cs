using Domain.Entidade;
using Domain.Repositorio;
using System;
using System.Collections.Generic;

namespace Domain.Service
{
    public class TrilhaService
    {
        private ITrilhaRepositorio RespositorioTrilha { get; }

        public TrilhaService(ITrilhaRepositorio trilhaRespositorio)
        {
            RespositorioTrilha = trilhaRespositorio;
        }

        public List<Trilha> GetAll()
        {
            return RespositorioTrilha.GetAll();
        }
        public Trilha GetPost(Guid id)
        {
            return RespositorioTrilha.GetById(id);
        }

        public Trilha CreateTrilha(Guid pessoaId, 
            string nomeTrilha, 
            string imagemTrilha, 
            string duracaoTrilha,
            DateTime dataTrilha,
            string local, 
            string nivel, 
            string descricao)
        {

            var resul = new Trilha();

            resul.Id = new Guid();
            resul.NomeTrilha = nomeTrilha;
            resul.ImagemTrilha = imagemTrilha;
            resul.DuracaoTrilha = duracaoTrilha;
            resul.DataTrilha = dataTrilha;
            resul.Local = local;
            resul.Nivel = nivel;
            resul.Descricao = descricao;
            resul.AddPessoaTrilha(pessoaId);

            RespositorioTrilha.SaveUpdate(resul);

            return resul;

        }

        public Trilha UpdateTrilha(Guid id,
            string nomeTrilha,
            string imagemTrilha,
            string duracaoTrilha,
            string local,
            string nivel,
            string descricao)
        {
            var trilha = RespositorioTrilha.GetById(id);

            trilha.NomeTrilha = nomeTrilha;
            trilha.ImagemTrilha = imagemTrilha;
            trilha.DuracaoTrilha = duracaoTrilha;
            trilha.Local = local;
            trilha.Nivel = nivel;
            trilha.Descricao = descricao;

            RespositorioTrilha.SaveUpdate(trilha);

            return trilha;
        }


        public void DeleteTrilha(Guid id)
        {
            RespositorioTrilha.Remove(id);
        }
    }
}
