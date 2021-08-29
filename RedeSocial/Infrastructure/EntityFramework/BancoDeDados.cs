using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entidade;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.EntityFramework
{
    public class BancoDeDados : DbContext
    {

        public BancoDeDados(DbContextOptions options) : base(options) { }

        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
    }
}
