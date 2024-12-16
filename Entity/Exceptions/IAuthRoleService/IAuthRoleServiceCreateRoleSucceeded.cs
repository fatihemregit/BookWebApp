using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IAuthRoleService
{
    public class IAuthRoleServiceCreateRoleSucceeded : Exception
    {
        public IAuthRoleServiceCreateRoleSucceeded(string? message) : base(message)
        {
        }
    }
}
