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
    public class RepositorioPessoa : IRepositorioPessoa
    {

        private BancoDeDados db { get; }

        public RepositorioPessoa(BancoDeDados bancoDeDados)
        {
            db = bancoDeDados;
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return db.Pessoa.AsNoTracking().ToList();
        }

        public Pessoa GetById(Guid id)
        {
            return db.Pessoa.Find(id);
        }

        public void Remove(Guid id)
        {
            var pessoa = db.Pessoa.Find(id);
            if (pessoa != null)
             {
                    db.Pessoa.Remove(pessoa);
                    db.SaveChanges();
             }
        }

        public void Save(Pessoa pessoa)
        {
            db.Pessoa.Add(pessoa);
            db.SaveChanges();
        }

        public void Update(Pessoa pessoa)
        {
            db.Pessoa.Update(pessoa);
            db.SaveChanges();
        }
    }
}
