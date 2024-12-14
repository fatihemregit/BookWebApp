using Data;
using Entity;
using Entity.Auth;
using Entity.IAuthUserRepository;
using Entity.IAuthUserService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts.Auth
{
    public interface IAuthUserService
	{
		Task<List<IAuthUserServiceGetAllUsersAsync>?> GetAllUsersAsync();
		Task<IAuthUserServiceFindByEmailAsync?> FindByEmailAsync(string email);
		Task<IAuthUserServiceFindByNameAsync?> FindByNameAsync(string userName);

		Task<bool> IsInRoleAsync(IAuthUserServiceIsInRoleAsync user, string roleName);

		Task<IList<string>?> GetRolesAsync(IAuthUserServiceGetRolesAsync user);

		Task<IdentityResult> CreateAsync(IAuthUserServiceCreateAsync user, string password);
		Task<IdentityResult> DeleteAsync(IAuthUserServiceDeleteAsync user);

		Task AddToRoleAsync(IAuthUserServiceAddToRoleAsync user, string roleName);

		Task RemoveFromRoleAsync(IAuthUserServiceRemoveFromRoleAsync user, string roleName);
        Task<List<IAuthUserServiceGetUsersInRoleAsync>?> GetUsersInRoleAsync(string roleName);

		Task<Exception> SignIn(IAuthUserServiceSignIn user);
		
		Task<Exception> Login(IAuthUserServiceLogin user);
		Task Logout();

		Task<Exception> DeleteUser(string userName);

		Task<bool> checkUserIsLogin(string userName);

		Task<IAuthUserServiceFindLocalUserwithUserName?> findLocalUserwithUserName(string userName);


    }

}