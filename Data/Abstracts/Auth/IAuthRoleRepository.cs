using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.IAuthRoleRepository;

namespace Data.Abstracts.Auth
{
	public interface IAuthRoleRepository
	{
		Task<IdentityResult> CreateAsync(IAuthRoleRepositoryCreateAsync role);
		Task<List<IAuthRoleRepositoryGetAllRolesAsync>?>GetAllRolesAsync();

		Task<IAuthRoleRepositoryFindByIdAsync?> FindByIdAsync(string roleId);

		Task DeleteAsync(IAuthRoleRepositoryDeleteAsync role);

	}


}