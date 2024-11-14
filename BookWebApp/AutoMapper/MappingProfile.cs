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
            CreateMap<BookViewModel,BookDto>();
            CreateMap<BookDto, BookViewModel>();
            //BookViewModelForUpdate to BookDto
            CreateMap<BookViewModelForUpdate, BookDto>();
            CreateMap<BookDto,BookViewModelForUpdate>();

        }
    }
}
