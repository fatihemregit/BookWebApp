using AutoMapper;
using Entity.ViewModel;
using Entity.Dto;
using Entity.Auth;
using Entity.IBookService;
using Entity.IAuthUserService;
using Entity.IAuthRoleService;
using Entity.IAuthUserRepository;
using System.Security.Claims;
using BookWebApp.Controllers;
using Business;
namespace BookWebApp.Utils.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //IBookServiceGetAllBook to BookViewModelForList
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

            //önemli yer
            //IAuthUserServiceGetAllUsersAsync to AppUserViewModel
            CreateMap<IAuthUserServiceGetAllUsersAsync, AppUserViewModel>();
            CreateMap<AppUserViewModel, IAuthUserServiceGetAllUsersAsync>();

            //IAuthUserServiceSignIn to AppUserViewModel
            CreateMap<IAuthUserServiceSignIn, AppUserViewModel>();
            CreateMap<AppUserViewModel, IAuthUserServiceSignIn>();
            //IAuthUserServiceLogin to LoginViewModel
            CreateMap<IAuthUserServiceLogin, LoginViewModel>();
            CreateMap<LoginViewModel, IAuthUserServiceLogin>();
            ////IAuthUserServiceIsInRoleAsync to IAuthUserServiceFindLocalUserwithUserName
            //CreateMap<IAuthUserServiceIsInRoleAsync, IAuthUserServiceFindLocalUserwithUserName>();
            //CreateMap<IAuthUserServiceFindLocalUserwithUserName, IAuthUserServiceIsInRoleAsync>();
            //IAuthRoleServiceCreateRolePost to CreateRoleViewModel
            CreateMap<IAuthRoleServiceCreateRolePost, CreateRoleViewModel>();
            CreateMap<CreateRoleViewModel, IAuthRoleServiceCreateRolePost>();
            //IAuthRoleServiceDeleteRolePost to DeleteRoleViewModel
            CreateMap<IAuthRoleServiceDeleteRolePost, DeleteRoleViewModel>();
            CreateMap<DeleteRoleViewModel, IAuthRoleServiceDeleteRolePost>();
            //IAuthRoleServiceSetRoleForUserGet to SetRoleForUserViewModel
            CreateMap<IAuthRoleServiceSetRoleForUserGet, SetRoleForUserViewModel>();
            CreateMap<SetRoleForUserViewModel, IAuthRoleServiceSetRoleForUserGet>();

            //IAuthRoleServiceSetRoleForUserPost to SetRoleForUserViewModel
            CreateMap<IAuthRoleServiceSetRoleForUserPost, SetRoleForUserViewModel>();
            CreateMap<SetRoleForUserViewModel, IAuthRoleServiceSetRoleForUserPost>();

        }
    }
}
