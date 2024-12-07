using AutoMapper;
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
		}
	}
}
