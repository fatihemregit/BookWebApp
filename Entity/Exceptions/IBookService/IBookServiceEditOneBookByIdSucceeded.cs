using Entity.IBookService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IBookService
{
    public class IBookServiceEditOneBookByIdSucceeded : Exception
    {
        public IBookServiceEditOneBookById  Book { get; set; }

        public IBookServiceEditOneBookByIdSucceeded(string? message, IBookServiceEditOneBookById book) : base(message)
        {
            Book = book;
        }
    }
}
