using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IBookService
{
    public class IBookServiceGetOneBookByIdNotSucceeded : Exception
    {
        public IBookServiceGetOneBookByIdNotSucceeded(string? message) : base(message)
        {
        }
    }
}
