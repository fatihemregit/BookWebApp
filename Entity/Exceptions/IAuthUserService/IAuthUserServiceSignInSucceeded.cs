using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IAuthUserService
{

    public class IAuthUserServiceSignInSucceeded : Exception
    {


        public IAuthUserServiceSignInSucceeded(string? message) : base(message)
        {
        }
    }
}
