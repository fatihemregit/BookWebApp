using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IAuthRoleService
{
    public class IAuthRoleServiceDeleteRoleGetNotSucceeded : Exception
    {
        public IAuthRoleServiceDeleteRoleGetNotSucceeded(string? message) : base(message)
        {
        }
    }
}
