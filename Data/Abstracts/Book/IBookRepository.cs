using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.IBookRepository;
using Entity.Dto;
namespace Data.Abstracts.Book
{


    public interface IBookRepository
    {
        Task<List<IBookRepositoryGetAllBook>> getAll();
        Task<IBookRepositoryCreateOneBook> createOneBook(IBookRepositoryCreateOneBook Book);
        Task<IBookRepositoryEditOneBookById?> editOneBookById(int id, IBookRepositoryEditOneBookById Book);
        Task<IBookRepositoryGetOneBookById?> getOneBookById(int id);
        Task deleteOneBookById(int id);





    }
}
