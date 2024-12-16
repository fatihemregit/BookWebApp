using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IAuthRoleService
{
    public class IAuthRoleServiceCreateRoleNotSucceeded : Exception
    {
        public IEnumerable<IdentityError> Errors { get; set; }

        public IAuthRoleServiceCreateRoleNotSucceeded(string? message, IEnumerable<IdentityError> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
