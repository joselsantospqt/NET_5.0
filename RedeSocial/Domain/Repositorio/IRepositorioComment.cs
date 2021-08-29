using System;
using Domain.Entidade;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IRepositorioComment
    {
        void Save(Comment comment);
        Comment GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Comment> GetAll();
        void Update(Comment comment);
    }
}
