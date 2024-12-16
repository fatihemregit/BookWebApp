using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IBookService
{
    public class IBookServiceDeleteOneBookByIdNotSucceeded : Exception
    {
        public IBookServiceDeleteOneBookByIdNotSucceeded(string? message) : base(message)
        {
        }
    }
}
