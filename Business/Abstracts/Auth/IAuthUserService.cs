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
		Task<Exception> SignIn(IAuthUserServiceSignIn user);
		
		Task<Exception> Login(IAuthUserServiceLogin user);
		Task Logout();

		Task<Exception> DeleteUser(string userName);

		Task<bool> checkUserIsLogin(string userName);

		Task<IAuthUserServiceFindLocalUserwithUserName?> findLocalUserwithUserName(string userName);

		Task<Dictionary<string, bool>> checkRoleswithLocalUserName(string userName, List<string> roleNames);


    }

}