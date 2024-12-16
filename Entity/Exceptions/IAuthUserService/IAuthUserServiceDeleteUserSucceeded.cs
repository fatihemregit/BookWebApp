using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IAuthUserService
{
    public class IAuthUserServiceDeleteUserSucceeded : Exception
    {
        public IAuthUserServiceDeleteUserSucceeded(string? message) : base(message)
        {
        }
    }
}
