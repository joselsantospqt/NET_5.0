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
    class RepositorioComment : IRepositorioComment
    {
        private BancoDeDados _db { get; }

        public RepositorioComment(BancoDeDados bancoDeDados)
        {
            _db = bancoDeDados;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _db.Comment.AsNoTracking().ToList();
        }

        public Comment GetById(Guid id)
        {
            return _db.Comment.Find(id);
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

        public void Save(Comment Comment)
        {
            _db.Comment.Add(Comment);
            _db.SaveChanges();
        }

        public void Update(Comment Comment)
        {
            _db.Comment.Update(Comment);
            _db.SaveChanges();
        }
    }
}
