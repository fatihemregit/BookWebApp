﻿using AutoMapper;
using Entity.ViewModel;
using Entity.Dto;
using Entity.Auth;
using Entity.IBookService;
using Entity.IAuthUserService;
namespace BookWebApp.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //BookViewModel to BookDto
            CreateMap<BookViewModelForList,BookDto>();
            CreateMap<BookDto, BookViewModelForList>();
            //BookViewModelForUpdate to BookDto
            CreateMap<BookViewModelForUpdate, BookDto>();
            CreateMap<BookDto,BookViewModelForUpdate>();

            //BookViewModelForCreate to BookDto
            CreateMap<BookViewModelForCreate, BookDto>();
            CreateMap<BookDto, BookViewModelForCreate>();

            //BookViewModelForDetails to BookDto
            CreateMap<BookViewModelForDetails, BookDto>();
            CreateMap<BookDto, BookViewModelForDetails>();

            //BookViewModelForDelete to BookDto
            CreateMap<BookViewModelForDelete, BookDto>();
            CreateMap<BookDto, BookViewModelForDelete>();

            //AppUserViewModel to AppUser
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppUserViewModel,AppUser>();

            //CreateRoleViewModel to AppRole
            CreateMap<AppRole, CreateRoleViewModel>();
			CreateMap<CreateRoleViewModel,AppRole >();


            //IBookServiceGetAllBook to BookViewModelForList
            CreateMap<List<IBookServiceGetAllBook>, List<BookViewModelForList>>();
            CreateMap<List<BookViewModelForList>, List<IBookServiceGetAllBook>>();
			CreateMap<IBookServiceGetAllBook, BookViewModelForList>();
			CreateMap<BookViewModelForList, IBookServiceGetAllBook>();

            //IBookServiceCreateOneBook to BookViewModelForCreate
            CreateMap<IBookServiceCreateOneBook, BookViewModelForCreate>();
            CreateMap<BookViewModelForCreate,IBookServiceCreateOneBook >();
            //IBookServiceGetOneBookById to BookViewModelForUpdate
            CreateMap<IBookServiceGetOneBookById, BookViewModelForUpdate>();
            CreateMap<BookViewModelForUpdate,IBookServiceGetOneBookById >();
            //IBookServiceEditOneBookById to BookViewModelForUpdate
            CreateMap<IBookServiceEditOneBookById, BookViewModelForUpdate>();
            CreateMap<BookViewModelForUpdate, IBookServiceEditOneBookById>();
            //IBookServiceGetOneBookById to BookViewModelForDetails
            CreateMap<IBookServiceGetOneBookById, BookViewModelForDetails>();
            CreateMap<BookViewModelForDetails, IBookServiceGetOneBookById>();
            //IBookServiceGetOneBookById to BookViewModelForDelete
            CreateMap<IBookServiceGetOneBookById, BookViewModelForDelete>();
            CreateMap<BookViewModelForDelete, IBookServiceGetOneBookById>();
            //IAuthUserServiceGetAllUsersAsync to AppUserViewModel
            CreateMap<IAuthUserServiceGetAllUsersAsync, AppUserViewModel>();
            CreateMap<AppUserViewModel, IAuthUserServiceGetAllUsersAsync>();
            //IAuthUserServiceCreateAsync to AppUserViewModel
            CreateMap<IAuthUserServiceCreateAsync, AppUserViewModel>();
            CreateMap<AppUserViewModel, IAuthUserServiceCreateAsync>();
            //IAuthUserServiceFindByEmailAsync to AppUser
            CreateMap<IAuthUserServiceFindByEmailAsync, AppUser>();
            CreateMap<AppUser, IAuthUserServiceFindByEmailAsync>();
            //IAuthUserServiceFindByNameAsync to AppUser
            CreateMap<IAuthUserServiceFindByNameAsync, AppUser>();
            CreateMap<AppUser, IAuthUserServiceFindByNameAsync>();
            //IAuthUserServiceDeleteAsync to AppUser
            CreateMap<IAuthUserServiceDeleteAsync, AppUser>();
            CreateMap<AppUser, IAuthUserServiceDeleteAsync>();


		}
	}
}
