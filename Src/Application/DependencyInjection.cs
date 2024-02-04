using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(conf=>
            {
                conf.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            });
            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

            return services;
        }
    }
}
