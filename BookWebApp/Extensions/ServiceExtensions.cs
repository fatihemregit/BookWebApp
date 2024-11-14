using BookWebApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookWebApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((options) => {
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection"));
            });
        }
    }
}
