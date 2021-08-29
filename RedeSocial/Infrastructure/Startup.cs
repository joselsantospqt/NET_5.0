using Domain.Repositorio;
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
            services.AddDbContext<BancoDeDados>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IRepositorioPost, RepositorioPost>();
            services.AddScoped<IRepositorioPessoa, RepositorioPessoa>();
            services.AddScoped<IRepositorioComment, RepositorioComment>();
        }
    }
}
