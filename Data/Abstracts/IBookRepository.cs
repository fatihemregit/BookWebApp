using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dto;
namespace Data.Abstracts
{


    public interface IBookRepository
	{
		Task<List<BookDto>> getAll();

		Task<BookDto> getOneBookById(int id);

		Task createOneBook(BookDto Book);

		Task editOneBookById(int id,BookDto book);

		Task deleteOneBookById(int id);



	}
}
