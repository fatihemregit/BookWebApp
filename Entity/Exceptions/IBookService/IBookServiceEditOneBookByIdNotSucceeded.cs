using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IBookService
{
    public class IBookServiceEditOneBookByIdNotSucceeded : Exception
    {
        public IBookServiceEditOneBookByIdNotSucceeded(string? message) : base(message)
        {
        }
    }
}
