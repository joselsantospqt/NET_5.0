using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface ITrilhaRepositorio
    {
        Trilha GetById(Guid id);
        void Remove(Guid id);
        List<Trilha> GetAll();
        void SaveUpdate(Trilha trilha);
    }
}
