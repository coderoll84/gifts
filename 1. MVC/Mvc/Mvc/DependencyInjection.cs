using Microsoft.EntityFrameworkCore;
using Mvc.Data.Context;
using Mvc.Data.Models;
using Mvc.DataAccess.Interfaces;
using Mvc.DataAccess.Servicios;

namespace Mvc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IRepositoryAsync<Ocasione>, RepositoryAsync<Ocasione>>();
            services.AddScoped<IRepositoryAsync<Rol>, RepositoryAsync<Rol>>();
            services.AddScoped<IRepositoryAsync<Usuario>, RepositoryAsync<Usuario>>();

            services.AddDbContext<PvContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("PvConnection"));

            });

            return services;
        }
    }
}
