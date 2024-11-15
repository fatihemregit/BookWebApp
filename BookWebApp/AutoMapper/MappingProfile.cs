using AutoMapper;
using BookWebApp.Models.Dto;
using BookWebApp.Models.ViewModel;

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


        }
    }
}
