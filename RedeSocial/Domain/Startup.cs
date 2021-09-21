using Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class Startup
    {
        public static void AddAplicationCore(this IServiceCollection services)
        {
            services.AddScoped<PostService>();
            services.AddScoped<PessoaService>();
            services.AddScoped<CommentService>();
            services.AddScoped<ImagemService>();

        }
    }
}
