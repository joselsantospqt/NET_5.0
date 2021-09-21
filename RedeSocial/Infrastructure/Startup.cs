using Domain.Repositorio;
using Infrastructure.BlobStorage;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class Startup
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BancoDeDados>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Infrastructure")));

            services.AddScoped<IPostRepositorio, PostRepositorio>();
            services.AddScoped<IPessoaRepositorio, PessoaRepositorio>();
            services.AddScoped<ICommentRepositorio, CommentRepositorio>();
        }
    }
}
