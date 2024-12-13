using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class IAuthUserServiceLoginNotSucceeded : Exception
    {
        public IAuthUserServiceLoginNotSucceeded(string? message) : base(message)
        {
        }
    }
}
