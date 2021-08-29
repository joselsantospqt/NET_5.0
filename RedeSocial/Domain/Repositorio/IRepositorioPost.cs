using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IRepositorioPost
    {
        void Save(Post post);
        Post GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<Post> GetAll();
        void Update(Post post);
    }
}
