using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.IAuthRoleService;

namespace Business.Abstracts.Auth
{
    public interface IAuthRoleService
    {
        Task<Exception> CreateRolePost(IAuthRoleServiceCreateRolePost role);

        Task<Exception> DeleteRoleGet();

        Task<Exception> DeleteRolePost(IAuthRoleServiceDeleteRolePost role);

        Task<Exception> SetRoleForUserGet(string userEmail);
        Task<Exception> SetRoleForUserPost(List<IAuthRoleServiceSetRoleForUserPost> roles,string userEmail, string localUserName);
    }

}
