using Entity.IAuthRoleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IAuthRoleService
{
    public class IAuthRoleServiceSetRoleForUserGetSucceeded : Exception
    {

        public List<IAuthRoleServiceSetRoleForUserGet> setroles { get; set; }

        public IAuthRoleServiceSetRoleForUserGetSucceeded(string? message, List<IAuthRoleServiceSetRoleForUserGet> setroles) : base(message)
        {
            this.setroles = setroles;
        }
    }
}
