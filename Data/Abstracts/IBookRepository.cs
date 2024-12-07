using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.IBookRepository;
using Entity.Dto;
namespace Data.Abstracts
{


    public interface IBookRepository
	{
		Task<List<GetAllBook>> getAll();

		Task<GetOneBookById> getOneBookById(int id);

		Task<CreateOneBook> createOneBook(CreateOneBook Book);

		Task<EditOneBookById> editOneBookById(int id,EditOneBookById Book);
		Task deleteOneBookById(int id);



	}
}
