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

        public IEnumerable<Trilha> GetAll()
        {
            return RespositorioTrilha.GetAll();
        }
        public Trilha GetPost(Guid id)
        {
            return RespositorioTrilha.GetById(id);
        }

        public Trilha CreateTrilha(Trilha trilha)
        {
            var resul = new Trilha
            {
                Id = new Guid(),
                NomeTrilha = trilha.NomeTrilha,
                ImagemTrilha = trilha.ImagemTrilha,
                DuracaoTrilha = trilha.DuracaoTrilha,
                DataTrilha = trilha.DataTrilha,
                Local = trilha.Local,
                Nivel = trilha.Nivel,
                Descricao = trilha.Descricao

            };

            RespositorioTrilha.SaveUpdate(trilha);

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
