using Domain;
using Domain.Entidade;
using Domain.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.Repositorio
{
    public class TrilhaRepositorio : ITrilhaRepositorio
    {
        private BancoDeDados _db { get; }


        public TrilhaRepositorio(BancoDeDados bancoDedados)
        {
            _db = bancoDedados;
        }

        public List<Trilha> GetAll()
        {
            return _db.Trilha.AsNoTracking().ToList();
        }


        public Trilha GetById(Guid id)
        {
            return _db.Trilha.Where(x => x.Id == id).FirstOrDefault();
        }

       
        public void SaveUpdate(Trilha trilha)
        {
            if (trilha.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                _db.Add(trilha);
            else
                _db.Update(trilha);

            _db.SaveChanges();
        }

        public void Remove(Guid id)
        {
            var trilha = _db.Trilha.Find(id);
            if (trilha != null)
            {
                _db.Trilha.Remove(trilha);
                _db.SaveChanges();
            }

        }

    }
}

