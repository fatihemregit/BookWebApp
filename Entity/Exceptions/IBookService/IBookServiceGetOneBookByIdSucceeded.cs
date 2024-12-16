using Entity.IBookService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IBookService
{
    public class IBookServiceGetOneBookByIdSucceeded : Exception
    {
        public IBookServiceGetOneBookById Book { get; set; }

        public IBookServiceGetOneBookByIdSucceeded(string? message, IBookServiceGetOneBookById book) : base(message)
        {
            Book = book;
        }
    }
}
