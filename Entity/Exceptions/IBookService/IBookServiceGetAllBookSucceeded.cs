using Entity.IBookService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IBookService
{
    public class IBookServiceGetAllBookSucceeded : Exception
    {

        public List<IBookServiceGetAllBook> AllBooks { get; set; }

        public IBookServiceGetAllBookSucceeded(string? message, List<IBookServiceGetAllBook> allBooks) : base(message)
        {
            AllBooks = allBooks;
        }
    }
}
