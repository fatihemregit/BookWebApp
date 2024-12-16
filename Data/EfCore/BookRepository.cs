using AutoMapper;
using Data.Abstracts.Book;
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

		public async Task<List<IBookRepositoryGetAllBook>> getAll()
		{
			return _mapper.Map<List<IBookRepositoryGetAllBook>>(await _context.BookDtos.ToListAsync());

		}


		public async Task<IBookRepositoryCreateOneBook> createOneBook(IBookRepositoryCreateOneBook Book)
		{

			await _context.BookDtos.AddAsync(_mapper.Map<BookDto>(Book));
			_context.SaveChanges();
			return Book;
		}

		public async Task<IBookRepositoryEditOneBookById?> editOneBookById(int id, IBookRepositoryEditOneBookById Book)
		{
			//IBookRepositoryGetOneBookById foundGetOneBookById = await getOneBookById(id);
			//if (foundGetOneBookById is null)
			//{
			//	return null;
			//}
			//foundGetOneBookById.Name = Book.Name;
			//foundGetOneBookById.Writer = Book.Writer;
			//foundGetOneBookById.Price = Book.Price;
			//_context.BookDtos.Update(_mapper.Map<BookDto>(foundGetOneBookById));
			//return _mapper.Map<IBookRepositoryEditOneBookById>(foundGetOneBookById);
			BookDto foundBookDto = await _context.BookDtos.Where(bd => bd.Id == id).SingleOrDefaultAsync();
			if (foundBookDto is null)
			{ 
				return null;
			}
			//Hayır,automapper ile yapmayı deneme.çalışmıyor :(
			foundBookDto.Name = Book.Name;
			foundBookDto.Writer = Book.Writer;
			foundBookDto.Price = Book.Price;
			_context.BookDtos.Update(foundBookDto);
			_context.SaveChanges();
			return _mapper.Map<IBookRepositoryEditOneBookById>(foundBookDto);

		}

		public async Task<IBookRepositoryGetOneBookById?> getOneBookById(int id)
		{
			//bulunan ögedeki değişikleri kaydetmek için illa dto mu lazım bak 
			BookDto foundbookDto = await _context.BookDtos.Where(bd => bd.Id == id).SingleOrDefaultAsync();
			if (foundbookDto is null)
			{
				//daha sonrasında(hata yönetimi eklendiğinde) hata fırlat ama şimdilik null döndürelim
				return null;
			}
			return _mapper.Map<IBookRepositoryGetOneBookById>(foundbookDto);

		}


		public async Task deleteOneBookById(int id)
		{

			BookDto foundBookDto = await _context.BookDtos.Where(bd => bd.Id == id).SingleOrDefaultAsync();
			if (foundBookDto is null)
			{
				return;
			}
			foundBookDto.isDeleted = true;
			_context.SaveChanges();
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
