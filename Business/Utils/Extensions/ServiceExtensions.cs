using Business.Abstracts.Auth;
using Business.Abstracts.Book;
using Business.Concretes.Auth;
using Business.Concretes.Book;
using Business.Utils.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utils.Extensions
{
    public static class ServiceExtensions
    {
        public static void setAutoMapperForBusinessLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfileForBusinessLayer));
        }
		public static void setInterfaceConcretesForBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IBookService,BookService>();
            services.AddScoped<IAuthUserService, AuthUserService>();
            services.AddScoped<IAuthRoleService, AuthRoleService>();
        }


	}
}
