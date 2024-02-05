using Application.Common.Interfaces;
using Application.Data;
using Domain.Primitives;
using Domain.Products;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration conriguration)
        {
            services.AddPersistence(conriguration);
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationdbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("StoreRelationalDB")));
            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationdbContext>());
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationdbContext>());

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IDiscountService, DiscountService>();

            services.AddScoped<ICacheService, CacheService>();

            return services;
        }

    }
}
