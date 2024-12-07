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
		}
	}
}
