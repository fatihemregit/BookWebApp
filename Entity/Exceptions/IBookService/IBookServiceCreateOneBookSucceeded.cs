using Entity.IBookService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IBookService
{
    public class IBookServiceCreateOneBookSucceeded : Exception
    {
        public IBookServiceCreateOneBook Book { get; set; }
        public IBookServiceCreateOneBookSucceeded(string? message, IBookServiceCreateOneBook book) : base(message)
        {
            Book = book;
        }
    }
}
