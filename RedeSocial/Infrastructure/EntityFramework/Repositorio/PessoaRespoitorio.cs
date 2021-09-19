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
    public class PessoaRepositorio : IPessoaRepositorio
    {

        private BancoDeDados _db { get; }

        public PessoaRepositorio(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _db.Pessoa.Include(x => x.Posts).Include(x => x.Amigos).ToList();
        }

        public Pessoa GetById(Guid id)
        {
            return _db.Pessoa.Include(x => x.Posts).Include(x => x.Amigos).Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var pessoa = _db.Pessoa.Find(id);
            if (pessoa != null)
            {
                _db.Pessoa.Remove(pessoa);
                _db.SaveChanges();
            }
        }

        public void SaveUpdate(Pessoa pessoa)
        {
            if (pessoa.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                _db.Add(pessoa);
            else
                _db.Update(pessoa);

            _db.SaveChanges();

        }        
    }
}
