using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IAuthUserService
{
    public class IAuthUserServiceLoginNotSucceeded : Exception
    {
        public IAuthUserServiceLoginNotSucceeded(string? message) : base(message)
        {
        }
    }
}
