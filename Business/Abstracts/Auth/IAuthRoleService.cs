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
        Task<IdentityResult> CreateAsync(IAuthRoleServiceCreateAsync role);
        Task<List<IAuthRoleServiceGetAllRolesAsync>?> GetAllRolesAsync();

        Task<IAuthRoleServiceFindByIdAsync?> FindByIdAsync(string roleId);

        Task DeleteAsync(IAuthRoleServiceDeleteAsync role);

        Task<Exception> CreateRolePost(IAuthRoleServiceCreateRolePost role);

        Task<Exception> DeleteRoleGet();

        Task<Exception> DeleteRolePost(IAuthRoleServiceDeleteRolePost role);

    }

}
