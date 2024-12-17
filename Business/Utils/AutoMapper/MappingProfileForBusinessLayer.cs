using AutoMapper;
using Data;
using Entity.Auth;
using Entity.IAuthRoleRepository;
using Entity.IAuthRoleService;
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
			CreateMap<IBookRepositoryGetAllBook, IBookServiceGetAllBook>();
			CreateMap<IBookServiceGetAllBook, IBookRepositoryGetAllBook>();
			//IBookServiceEditOneBookById to IBookRepositoryEditOneBookById
			CreateMap<IBookRepositoryEditOneBookById, IBookServiceEditOneBookById>();
			CreateMap<IBookServiceEditOneBookById, IBookRepositoryEditOneBookById>();
			//IBookServiceCreateOneBook to IBookRepositoryCreateOneBook
			CreateMap<IBookServiceCreateOneBook, IBookRepositoryCreateOneBook>();
			CreateMap<IBookRepositoryCreateOneBook, IBookServiceCreateOneBook>();

            //ÖNEMLİ YER
            //IAuthUserServiceGetAllUsersAsync to IAuthUserRepositoryGetAllUsersAsync
            CreateMap<IAuthUserServiceGetAllUsersAsync, IAuthUserRepositoryGetAllUsersAsync>();
            CreateMap<IAuthUserRepositoryGetAllUsersAsync, IAuthUserServiceGetAllUsersAsync>();

            //IAuthUserServiceSignIn to IAuthUserRepositoryCreateAsync
            CreateMap<IAuthUserServiceSignIn, IAuthUserRepositoryCreateAsync>();
			CreateMap<IAuthUserRepositoryCreateAsync, IAuthUserServiceSignIn>();
			//IAuthUserRepositoryFindByNameAsync to IAuthUserRepositoryDeleteAsync
			CreateMap<IAuthUserRepositoryFindByNameAsync, IAuthUserRepositoryDeleteAsync>();
			CreateMap<IAuthUserRepositoryDeleteAsync, IAuthUserRepositoryFindByNameAsync>();

			//IAuthUserRepositoryFindByEmailAsync to AppUser
			CreateMap<IAuthUserRepositoryFindByEmailAsync, AppUser>();
			CreateMap<AppUser, IAuthUserRepositoryFindByEmailAsync>();

			//IAuthUserServiceFindLocalUserwithUserName to  IAuthUserRepositoryFindByNameAsync
			CreateMap<IAuthUserServiceFindLocalUserwithUserName, IAuthUserRepositoryFindByNameAsync>();
			CreateMap<IAuthUserRepositoryFindByNameAsync, IAuthUserServiceFindLocalUserwithUserName>();

			//IAuthRoleRepositoryCreateAsync to IAuthRoleServiceCreateRole
			CreateMap<IAuthRoleRepositoryCreateAsync, IAuthRoleServiceCreateRolePost>();
			CreateMap<IAuthRoleServiceCreateRolePost, IAuthRoleRepositoryCreateAsync>();

            //IAuthRoleServiceGetAllRolesAsync to IAuthRoleRepositoryGetAllRolesAsync
            //CreateMap<List<IAuthRoleServiceGetAllRolesAsync>, List<IAuthRoleRepositoryGetAllRolesAsync>>();
            //CreateMap<List<IAuthRoleRepositoryGetAllRolesAsync>, List<IAuthRoleServiceGetAllRolesAsync>>();
            CreateMap<IAuthRoleServiceGetAllRolesAsync, IAuthRoleRepositoryGetAllRolesAsync>();
            CreateMap<IAuthRoleRepositoryGetAllRolesAsync, IAuthRoleServiceGetAllRolesAsync>();

			//IAuthRoleRepositoryDeleteAsync to IAuthRoleRepositoryFindByIdAsync
			CreateMap<IAuthRoleRepositoryDeleteAsync, IAuthRoleRepositoryFindByIdAsync>();
			CreateMap<IAuthRoleRepositoryFindByIdAsync, IAuthRoleRepositoryDeleteAsync>();

			//IAuthUserRepositoryGetRolesAsync to IAuthUserRepositoryFindByEmailAsync
			CreateMap<IAuthUserRepositoryGetRolesAsync, IAuthUserRepositoryFindByEmailAsync>();
			CreateMap<IAuthUserRepositoryFindByEmailAsync, IAuthUserRepositoryGetRolesAsync>();

			// IAuthUserRepositoryAddToRoleAsync to IAuthUserRepositoryFindByEmailAsync
			CreateMap<IAuthUserRepositoryAddToRoleAsync, IAuthUserRepositoryFindByEmailAsync>();
			CreateMap<IAuthUserRepositoryFindByEmailAsync, IAuthUserRepositoryAddToRoleAsync>();
            // IAuthUserRepositoryRemoveFromRoleAsync to IAuthUserRepositoryFindByEmailAsync
           
			CreateMap<IAuthUserRepositoryRemoveFromRoleAsync, IAuthUserRepositoryFindByEmailAsync>();
            CreateMap<IAuthUserRepositoryFindByEmailAsync, IAuthUserRepositoryRemoveFromRoleAsync>();

			//IAuthUserRepositoryIsInRoleAsync to IAuthUserServiceFindLocalUserwithUserName
			CreateMap<IAuthUserRepositoryIsInRoleAsync, IAuthUserServiceFindLocalUserwithUserName>();
			CreateMap<IAuthUserServiceFindLocalUserwithUserName, IAuthUserRepositoryIsInRoleAsync>();

        }
    }
}
