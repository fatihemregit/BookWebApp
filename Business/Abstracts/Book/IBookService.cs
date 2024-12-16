using Entity.IBookService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts.Book
{
    public interface IBookService
    {
        Task<Exception> getAll();
        Task<Exception> createOneBook(IBookServiceCreateOneBook Book);
        Task<Exception> editOneBookById(int id, IBookServiceEditOneBookById Book);
        Task<Exception> getOneBookById(int id);
        Task<Exception> deleteOneBookById(int id);

    }
}
