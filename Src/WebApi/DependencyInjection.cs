using Application.Data;
using Domain.Primitives;
using Domain.Products;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;

namespace WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddW3CLogging(logging =>
            {
                logging.LoggingFields = W3CLoggingFields.TimeTaken;

                logging.FileSizeLimit = 5 * 1024 * 1024;
                logging.RetainedFileCountLimit = 2;
                logging.FileName = "ProductStoreLogFile_";
                logging.LogDirectory = configuration.GetValue<string>("TimeRequestLogPath");
                logging.FlushInterval = TimeSpan.FromSeconds(2);
            });

            return services;
        }

        
    }
}
