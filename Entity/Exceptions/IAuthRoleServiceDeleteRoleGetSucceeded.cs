using Entity.IAuthRoleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class IAuthRoleServiceDeleteRoleGetSucceeded : Exception
    {

        public List<IAuthRoleServiceGetAllRolesAsync> Roles { get; set; }
        public IAuthRoleServiceDeleteRoleGetSucceeded(string? message, List<IAuthRoleServiceGetAllRolesAsync> roles) : base(message)
        {
            Roles = roles;
        }
    }
}
