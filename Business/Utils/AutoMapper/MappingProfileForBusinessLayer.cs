using AutoMapper;
using Entity.IAuthUserRepository;
using Entity.IAuthUserService;
using Entity.IBookRepository;
using Entity.IBookService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utils.AutoMapper
{
	public class MappingProfileForBusinessLayer:Profile
	{
		/*
		 tdestination gerekli öge
         tsource kaynak
		*/

		public MappingProfileForBusinessLayer()
        {

			//service to repository

			//IBookServiceGetOneBookById to IBookRepositoryGetOneBookById
			CreateMap<IBookRepositoryGetOneBookById, IBookServiceGetOneBookById>();
			CreateMap<IBookServiceGetOneBookById, IBookRepositoryGetOneBookById>();
			//IBookServiceGetAllBook to IBookRepositoryGetAllBook
			CreateMap<List<IBookRepositoryGetAllBook>, List<IBookServiceGetAllBook>>();
			CreateMap<List<IBookServiceGetAllBook>,List<IBookRepositoryGetAllBook>>();
			CreateMap<IBookRepositoryGetAllBook, IBookServiceGetAllBook>();
			CreateMap<IBookServiceGetAllBook, IBookRepositoryGetAllBook>();
			//IBookServiceEditOneBookById to IBookRepositoryEditOneBookById
			CreateMap<IBookRepositoryEditOneBookById, IBookServiceEditOneBookById>();
			CreateMap<IBookServiceEditOneBookById, IBookRepositoryEditOneBookById>();
			//IBookServiceCreateOneBook to IBookRepositoryCreateOneBook
			CreateMap<IBookServiceCreateOneBook, IBookRepositoryCreateOneBook>();
			CreateMap<IBookRepositoryCreateOneBook, IBookServiceCreateOneBook>();
			//IAuthUserServiceCreateAsync to IAuthUserRepositoryCreateAsync
			CreateMap<IAuthUserServiceCreateAsync, IAuthUserRepositoryCreateAsync>();
			CreateMap<IAuthUserRepositoryCreateAsync, IAuthUserServiceCreateAsync>();
			//IAuthUserServiceDeleteAsync to IAuthUserRepositoryDeleteAsync
			CreateMap<IAuthUserServiceDeleteAsync, IAuthUserRepositoryDeleteAsync>();
			CreateMap<IAuthUserRepositoryDeleteAsync, IAuthUserServiceDeleteAsync>();
			//IAuthUserServiceFindByEmailAsync to IAuthUserRepositoryFindByEmailAsync
			CreateMap<IAuthUserServiceFindByEmailAsync, IAuthUserRepositoryFindByEmailAsync>();
			CreateMap<IAuthUserRepositoryFindByEmailAsync, IAuthUserServiceFindByEmailAsync>();
			//IAuthUserServiceFindByNameAsync to IAuthUserRepositoryFindByNameAsync
			CreateMap<IAuthUserServiceFindByNameAsync, IAuthUserRepositoryFindByNameAsync>();
			CreateMap<IAuthUserRepositoryFindByNameAsync, IAuthUserServiceFindByNameAsync>();
			//IAuthUserServiceGetAllUsersAsync to IAuthUserRepositoryGetAllUsersAsync
			CreateMap<List<IAuthUserServiceGetAllUsersAsync>, List<IAuthUserRepositoryGetAllUsersAsync>>();
			CreateMap<List<IAuthUserRepositoryGetAllUsersAsync>, List<IAuthUserServiceGetAllUsersAsync>>();
			CreateMap<IAuthUserServiceGetAllUsersAsync, IAuthUserRepositoryGetAllUsersAsync>();
			CreateMap<IAuthUserRepositoryGetAllUsersAsync, IAuthUserServiceGetAllUsersAsync>();
			//IAuthUserServiceGetRolesAsync to IAuthUserRepositoryGetRolesAsync
			CreateMap<IAuthUserServiceGetRolesAsync, IAuthUserRepositoryGetRolesAsync>();
			CreateMap<IAuthUserRepositoryGetRolesAsync, IAuthUserServiceGetRolesAsync>();
			//IAuthUserServiceIsInRoleAsync to IAuthUserRepositoryIsInRoleAsync
			CreateMap<IAuthUserServiceIsInRoleAsync, IAuthUserRepositoryIsInRoleAsync>();
			CreateMap<IAuthUserRepositoryIsInRoleAsync, IAuthUserServiceIsInRoleAsync>();

		}
	}
}
