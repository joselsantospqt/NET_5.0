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

    public class PostRepositorio : IPostRepositorio
    {
        private BancoDeDados _db { get; }


        public PostRepositorio(BancoDeDados bancoDedados)
        {
            _db = bancoDedados;
        }

        public IEnumerable<Post> GetAll()
        {
            return _db.Post.Include(x => x.Autor).Include(x => x.Comments).AsNoTracking().ToList();
        }

        public Post GetById(Guid id)
        {
            return _db.Post.Include(x => x.Autor).Include(x => x.Comments).Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var post = _db.Post.Find(id);
            if (post != null)
            {
                _db.Post.Remove(post);
                _db.SaveChanges();
            }

        }

        public void SaveUpdate(Post post)
        {
            if (post.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                _db.Add(post);
            else
                _db.Update(post);

            _db.SaveChanges();

        }
    }
}
