using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationdbContext>();

            dbContext.Database.Migrate();
        }
    }
}
