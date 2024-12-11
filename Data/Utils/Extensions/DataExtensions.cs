using Data.Abstracts.Auth;
using Data.Abstracts.Book;
using Data.EfCore;
using Data.EfCore.Auth;
using Data.EfCore.Context;
using Data.Utils.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils.Extensions
{
    public static class DataExtensions
    {
        public static void setAutoMapperForDataLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfileForDataLayer));
        }
        
        public static void setInterfaceConcretesForDataLayer(this IServiceCollection services)
        {
			services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            services.AddScoped<IAuthRoleRepository, AuthRoleRepository>();
		}
		public static void ConfigureSqlContextForDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
			services.AddDbContext<ApplicationDbContext>((options) =>
			{
				options.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                    x => x.MigrationsAssembly("Data")
                    );
			});
		}



	}
}
