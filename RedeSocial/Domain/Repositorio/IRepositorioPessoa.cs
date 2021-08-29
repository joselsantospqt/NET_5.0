using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IRepositorioPessoa
    {
        void Save(Pessoa pessoa);
        Pessoa GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Pessoa> GetAll();
        void Update(Pessoa pessoa);

    }
}
