﻿using Data.Abstracts;
using Data.EfCore;
using Data.Utils.AutoMapper;
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
			services.AddSingleton<IBookRepository, BookRepository>();
		}

	}
}
