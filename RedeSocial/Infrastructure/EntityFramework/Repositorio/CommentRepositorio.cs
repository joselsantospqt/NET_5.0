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
    class CommentRepositorio : ICommentRepositorio
    {
        private BancoDeDados _db { get; }

        public CommentRepositorio(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _db.Comment.Include(x => x.Post).Include(x => x.Pessoa).AsNoTracking().ToList();
        }

        public Comment GetById(Guid id)
        {
            return _db.Comment.Include(x => x.Post).Include(x => x.Pessoa).Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var Comment = _db.Comment.Find(id);
            if (Comment != null)
            {
                _db.Comment.Remove(Comment);
                _db.SaveChanges();
            }
        }
        public void SaveUpdate(Comment comment)
        {
            if (comment.Id.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                _db.Add(comment);
            else
                _db.Update(comment);

            _db.SaveChanges();

        }
    }
}
