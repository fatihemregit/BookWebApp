using Data.Abstracts;
using Data.EfCore.Context;
using Entity.Dto;
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

		public BookRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task createOneBook(BookDto Book)
		{
			await _context.BookDtos.AddAsync(Book);
		}

		public async Task deleteOneBookById(int id)
		{
			BookDto foundBookDto = await this.getOneBookById(id);
			foundBookDto.isDeleted = true;
		}

		public async Task editOneBookById(int id, BookDto book)
		{
			BookDto foundBookDto = await this.getOneBookById(id);
			foundBookDto.Name = book.Name;
			foundBookDto.Writer = book.Writer;
			foundBookDto.Price = book.Price;
			foundBookDto.isDeleted = book.isDeleted;
			_context.SaveChanges();
		}

		public async Task<List<BookDto>> getAll()
		{
			return await _context.BookDtos.ToListAsync();
		}

		public async Task<BookDto> getOneBookById(int id)
		{
			BookDto foundbookDto = _context.BookDtos.Where(bd => bd.Id == id).SingleOrDefault();
			if (foundbookDto is null) {
				//daha sonrasında(hata yönetimi eklendiğinde) hata fırlat
				return new BookDto();
			}
			return foundbookDto;
		}
	}
}
