using AutoMapper;
using Data.Abstracts;
using Data.EfCore.Context;
using Entity.Dto;
using Entity.IBookRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EfCore
{
    public class BookRepository : IBookRepository
	{

		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public BookRepository(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<CreateOneBook> createOneBook(CreateOneBook Book)
		{
			await _context.BookDtos.AddAsync(_mapper.Map<BookDto>(Book));
			return Book;
		}

		public async Task deleteOneBookById(int id)
		{
			BookDto foundBookDto = _mapper.Map<BookDto>(await getOneBookById(id));
			foundBookDto.isDeleted = true;
			_context.SaveChanges();
		}

		public async Task<EditOneBookById> editOneBookById(int id, EditOneBookById Book)
		{
			GetOneBookById foundGetOneBookById = await getOneBookById(id);
			foundGetOneBookById.Name = Book.Name;
			foundGetOneBookById.Writer = Book.Writer;
			foundGetOneBookById.Price = Book.Price;
			_context.BookDtos.Update(_mapper.Map<BookDto>(foundGetOneBookById));
			return _mapper.Map<EditOneBookById>(foundGetOneBookById);
		}

		public async Task<List<GetAllBook>> getAll()
		{
			return _mapper.Map<List<GetAllBook>>(await _context.BookDtos.ToListAsync());

		}

		public async Task<GetOneBookById> getOneBookById(int id)
		{
			//bulunan ögedeki değişikleri kaydetmek için illa dto mu lazım bak 
			BookDto foundbookDto = await _context.BookDtos.Where(bd => bd.Id == id).SingleOrDefaultAsync();
			if (foundbookDto is null)
			{
				//daha sonrasında(hata yönetimi eklendiğinde) hata fırlat
				return _mapper.Map<GetOneBookById>(new BookDto());
			}
			return _mapper.Map<GetOneBookById>(foundbookDto);

		}







		//eski kodlar
		//private readonly ApplicationDbContext _context;

		//public BookRepository(ApplicationDbContext context)
		//{
		//	_context = context;
		//}

		//public async Task createOneBook(BookDto Book)
		//{
		//	await _context.BookDtos.AddAsync(Book);
		//}

		//public async Task deleteOneBookById(int id)
		//{
		//	BookDto foundBookDto = await this.getOneBookById(id);
		//	foundBookDto.isDeleted = true;
		//}

		//public async Task editOneBookById(int id, BookDto book)
		//{
		//	BookDto foundBookDto = await this.getOneBookById(id);
		//	foundBookDto.Name = book.Name;
		//	foundBookDto.Writer = book.Writer;
		//	foundBookDto.Price = book.Price;
		//	foundBookDto.isDeleted = book.isDeleted;
		//	_context.SaveChanges();
		//}

		//public async Task<List<BookDto>> getAll()
		//{
		//	return await _context.BookDtos.ToListAsync();
		//}

		//public async Task<BookDto> getOneBookById(int id)
		//{
		//	BookDto foundbookDto = await _context.BookDtos.Where(bd => bd.Id == id).SingleOrDefaultAsync();
		//	if (foundbookDto is null) {
		//		//daha sonrasında(hata yönetimi eklendiğinde) hata fırlat
		//		return new BookDto();
		//	}
		//	return foundbookDto;
		//}
	}
}
