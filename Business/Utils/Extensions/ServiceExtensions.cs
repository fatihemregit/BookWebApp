using Business.Abstracts;
using Business.Concretes;
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
            services.AddSingleton<IBookService,BookService>();
        }


	}
}
