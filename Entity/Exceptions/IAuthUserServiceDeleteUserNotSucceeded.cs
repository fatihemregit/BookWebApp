using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class IAuthUserServiceDeleteUserNotSucceeded : Exception
    {
        public IAuthUserServiceDeleteUserNotSucceeded(string? message) : base(message)
        {
        }
    }
}
