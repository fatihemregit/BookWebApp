using Entity.IBookService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
	public interface IBookService
	{
		Task<List<IBookServiceGetAllBook>> getAll();
		Task<IBookServiceCreateOneBook?> createOneBook(IBookServiceCreateOneBook Book);
		Task<IBookServiceEditOneBookById?> editOneBookById(int id, IBookServiceEditOneBookById Book);
		Task<IBookServiceGetOneBookById?> getOneBookById(int id);
		Task deleteOneBookById(int id);


	}
}
