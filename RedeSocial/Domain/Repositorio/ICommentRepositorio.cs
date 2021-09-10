using System;
using Domain.Entidade;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface ICommentRepositorio
    {
        Comment GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Comment> GetAll();
        void SaveUpdate(Comment comment);

    }
}
