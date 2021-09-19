using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IPessoaRepositorio
    {
        Pessoa GetById(Guid id);
        Pessoa GetByEmail(string Email);
        void Remove(Guid id);
        IEnumerable<Pessoa> GetAll();
        void SaveUpdate(Pessoa pessoa);

    }
}
