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

    public class RepositorioPost : IRepositorioPost
    {
        private BancoDeDados db {get;}


        public RepositorioPost(BancoDeDados bancoDedados)
        {
            db = bancoDedados;
        }

        public IEnumerable<Post> GetAll()
        {
            return db.Post.AsNoTracking().ToList();
        }

        public Post GetById(Guid id)
        {
            return db.Post.Find(id);
        }

        public void  Remove(Guid id)
        {
            var post = db.Post.Find(id);
            if (post != null)
            {
                db.Post.Remove(post);
                db.SaveChanges();
            }

        }

        public void Save(Post post)
        {
            db.Post.Add(post);
            db.SaveChanges();
        }

        public void Update(Post post)
        {
            db.Post.Update(post);
            db.SaveChanges();
        }
    }
}
