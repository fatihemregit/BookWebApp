using AutoMapper;
using Entity.Dto;
using Entity.IBookRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils.AutoMapper
{
	public class MappingProfile : Profile
	{
		/*
		 tdestination gerekli öge
         tsource kaynak
		*/
		public MappingProfile()
		{
			//GetOneBookById to BookDto
			CreateMap<BookDto, GetOneBookById>();
			CreateMap<GetOneBookById, BookDto>();
			//GetAllBook to BookDto
			CreateMap<List<BookDto>, List<GetAllBook>>();
			CreateMap<List<GetAllBook>, List<BookDto>>();
			CreateMap<BookDto, GetAllBook>();
			CreateMap<GetAllBook, BookDto>();
			//EditOneBookById to GetOneBookById
			CreateMap<EditOneBookById, GetOneBookById>();
			CreateMap<GetOneBookById, EditOneBookById>();
			//CreateOneBook to BookDto
			CreateMap<CreateOneBook, BookDto>();
			CreateMap<BookDto, CreateOneBook>();
		}
	}
}
