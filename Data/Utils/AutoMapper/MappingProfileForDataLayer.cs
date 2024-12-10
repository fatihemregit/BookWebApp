using AutoMapper;
using Data.Abstracts.Auth;
using Entity.Auth;
using Entity.Dto;
using Entity.IBookRepository;
using Entity.IAuthUserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.IAuthRoleRepository;

namespace Data.Utils.AutoMapper
{
	public class MappingProfileForDataLayer : Profile
	{
		/*
		 tdestination gerekli öge
         tsource kaynak
		*/
		public MappingProfileForDataLayer()
		{
			//GetOneBookById to BookDto
			CreateMap<BookDto, IBookRepositoryGetOneBookById>();
			CreateMap<IBookRepositoryGetOneBookById, BookDto>();
			//GetAllBook to BookDto
			CreateMap<List<BookDto>, List<IBookRepositoryGetAllBook>>();
			CreateMap<List<IBookRepositoryGetAllBook>, List<BookDto>>();
			CreateMap<BookDto, IBookRepositoryGetAllBook>();
			CreateMap<IBookRepositoryGetAllBook, BookDto>();
			//EditOneBookById to GetOneBookById
			CreateMap<IBookRepositoryEditOneBookById, IBookRepositoryGetOneBookById>();
			CreateMap<IBookRepositoryGetOneBookById, IBookRepositoryEditOneBookById>();
			//CreateOneBook to BookDto
			CreateMap<IBookRepositoryCreateOneBook, BookDto>();
			CreateMap<BookDto, IBookRepositoryCreateOneBook>();
			//IAuthUserRepositoryCreateAsync to AppUser
			CreateMap<IAuthUserRepositoryCreateAsync, AppUser>();
			CreateMap<AppUser, IAuthUserRepositoryCreateAsync>();
			//IAuthUserRepositoryDeleteAsync to AppUser
			CreateMap<IAuthUserRepositoryDeleteAsync, AppUser>();
			CreateMap<AppUser, IAuthUserRepositoryDeleteAsync>();
			//IAuthUserRepositoryFindByEmailAsync to AppUser
			CreateMap<IAuthUserRepositoryFindByEmailAsync, AppUser>();
			CreateMap<AppUser, IAuthUserRepositoryFindByEmailAsync>();
			//IAuthUserRepositoryFindByNameAsync to AppUser
			CreateMap<IAuthUserRepositoryFindByNameAsync, AppUser>();
			CreateMap<AppUser, IAuthUserRepositoryFindByNameAsync>();
			//IAuthUserRepositoryGetAllUsers to AppUser
			CreateMap<List<IAuthUserRepositoryGetAllUsersAsync>, List<AppUser>>();
			CreateMap<List<AppUser>, List<IAuthUserRepositoryGetAllUsersAsync>>();
			CreateMap<IAuthUserRepositoryGetAllUsersAsync, AppUser>();
			CreateMap<AppUser, IAuthUserRepositoryGetAllUsersAsync>();
			//IAuthUserRepositoryGetRolesAsync to AppUser
			CreateMap<IAuthUserRepositoryGetRolesAsync, AppUser>();
			CreateMap<AppUser, IAuthUserRepositoryGetRolesAsync>();
			//IAuthUserRepositoryIsInRoleAsync to AppUser
			CreateMap<IAuthUserRepositoryIsInRoleAsync, AppUser>();
			CreateMap<AppUser, IAuthUserRepositoryIsInRoleAsync>();
			//IAuthRoleRepositoryCreateAsync to AppRole
			CreateMap<IAuthRoleRepositoryCreateAsync, AppRole>();
			CreateMap<AppRole, IAuthRoleRepositoryCreateAsync>();
			//IAuthRoleRepositoryDeleteAsync to AppRole
			CreateMap<IAuthRoleRepositoryDeleteAsync, AppRole>();
			CreateMap<AppRole, IAuthRoleRepositoryDeleteAsync>();
			//IAuthRoleRepositoryFindByIdAsync to AppRole
			CreateMap<IAuthRoleRepositoryFindByIdAsync, AppRole>();
			CreateMap<AppRole, IAuthRoleRepositoryFindByIdAsync>();
			//IAuthRoleRepositoryGetAllRolesAsync to AppRole
			CreateMap<List<IAuthRoleRepositoryGetAllRolesAsync>, List<AppRole>>();
			CreateMap<List<AppRole>, List<IAuthRoleRepositoryGetAllRolesAsync>>();
			CreateMap<IAuthRoleRepositoryGetAllRolesAsync, AppRole>();
			CreateMap<AppRole, IAuthRoleRepositoryGetAllRolesAsync>();

		}
	}
}
