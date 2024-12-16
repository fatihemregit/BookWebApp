using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IAuthRoleService
{
    public class IAuthRoleServiceSetRoleForUserPostSucceeded : Exception
    {
        public IAuthRoleServiceSetRoleForUserPostSucceeded(string? message) : base(message)
        {
        }
    }
}
