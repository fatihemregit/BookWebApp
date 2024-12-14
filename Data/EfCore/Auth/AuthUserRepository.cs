using AutoMapper;
using Data.Abstracts.Auth;
using Entity.Auth;
using Entity.IAuthUserRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EfCore.Auth
{
	public class AuthUserRepository : IAuthUserRepository
	{

		private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly IMapper _mapper;

		public AuthUserRepository(UserManager<AppUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}

		public async Task AddToRoleAsync(IAuthUserRepositoryAddToRoleAsync user, string roleName)
		{
            Console.WriteLine($"AuthUserRepository/AddToRoleAsync metodu çalıştı ( {roleName} rolü için )");
			AppUser foundUser = await _userManager.FindByIdAsync(user.Id.ToString());
			IdentityResult identityResult =  await _userManager.AddToRoleAsync(foundUser,roleName);
			foreach (var item in identityResult.Errors)
			{
                Console.WriteLine($"Hata Kodu :{item.Code}\nhata açıklaması: {item.Description}");
			}
		}

		public async Task<IdentityResult> CreateAsync(IAuthUserRepositoryCreateAsync user, string password)
		{
			IdentityResult result = await _userManager.CreateAsync(_mapper.Map<AppUser>(user), password);
			return result;
		}

		public async Task<IdentityResult?> DeleteAsync(IAuthUserRepositoryDeleteAsync user)
		{
			AppUser? foundUser = await _userManager.FindByIdAsync(user.Id.ToString());
			if(foundUser == null)
			{
				return null;
			}
			IdentityResult result = await _userManager.DeleteAsync(foundUser);
			return result;
		}

		public async Task<IAuthUserRepositoryFindByEmailAsync?> FindByEmailAsync(string email)
		{
			AppUser? foundappUser = await _userManager.FindByEmailAsync(email);
			//neden burada null check yapıyoruz ki?? bu null check i business te yapmamız gerekmez mi.sonuçta iş kuralı
			if (foundappUser is null)
			{
				return null;
			}
			IAuthUserRepositoryFindByEmailAsync findByEmailAsync = _mapper.Map<IAuthUserRepositoryFindByEmailAsync>(foundappUser);
			IList<string> rolesfromUserManager = await _userManager.GetRolesAsync(foundappUser);
			findByEmailAsync.Roles = (rolesfromUserManager.Count <= 0) ? new List<string>() : rolesfromUserManager;
			return findByEmailAsync;
		}

        public async Task<AppUser?> FindByEmailAsyncAndReturnAppUser(string email)
        {
			AppUser? foundUser =  await _userManager.FindByEmailAsync(email);
			if (foundUser is null)
			{
				return null;
			}
			return foundUser;

        }

        public async Task<IAuthUserRepositoryFindByNameAsync?> FindByNameAsync(string userName)
		{
			AppUser? foundappUser = await _userManager.FindByNameAsync(userName);
			if (foundappUser is null)
			{
				return null;
			}
			IAuthUserRepositoryFindByNameAsync findByNameAsync = _mapper.Map<IAuthUserRepositoryFindByNameAsync>(foundappUser);
			IList<string> rolesfromUserManager = await _userManager.GetRolesAsync(foundappUser);
			findByNameAsync.Roles = (rolesfromUserManager.Count <= 0) ? new List<string>() : rolesfromUserManager;
			return findByNameAsync;
		}

		public async Task<List<IAuthUserRepositoryGetAllUsersAsync>?> GetAllUsersAsync()
		{
			List<AppUser> appUsers = await _userManager.Users.ToListAsync();
			if (appUsers is null)
			{
				return null;
			}
			List<IAuthUserRepositoryGetAllUsersAsync> getAllUsers = _mapper.Map<List<IAuthUserRepositoryGetAllUsersAsync>>(appUsers);
			foreach (IAuthUserRepositoryGetAllUsersAsync user in getAllUsers)
			{
				IList<string> rolesfromUserManager = await _userManager.GetRolesAsync(_mapper.Map<AppUser>(user));
				user.Roles = (rolesfromUserManager.Count <= 0) ? new List<string>() : rolesfromUserManager;
			}
			return getAllUsers;
		}

		public async Task<IList<string>?> GetRolesAsync(IAuthUserRepositoryGetRolesAsync user)
		{
			IList<string> userRoles = await _userManager.GetRolesAsync(_mapper.Map<AppUser>(user));
			if (userRoles.Count <= 0)
			{
				return null;
			}
			return userRoles;


		}

        public async Task<List<IAuthUserRepositoryGetUsersInRoleAsync>?> GetUsersInRoleAsync(string roleName)
        {
			List<AppUser> getUsersInRoleAsync = (await _userManager.GetUsersInRoleAsync(roleName)).ToList();
			if (getUsersInRoleAsync.Count <= 0)
			{
				return null;
			}
			return _mapper.Map<List<IAuthUserRepositoryGetUsersInRoleAsync>>(getUsersInRoleAsync);
        }

        public async Task<bool> IsInRoleAsync(IAuthUserRepositoryIsInRoleAsync user, string roleName)
		{

			bool result = await _userManager.IsInRoleAsync(_mapper.Map<AppUser>(user), roleName);
			return result;
		}

		public async Task RemoveFromRoleAsync(IAuthUserRepositoryRemoveFromRoleAsync user, string roleName)
		{
			AppUser foundUser = await _userManager.FindByIdAsync(user.Id.ToString());
			await _userManager.RemoveFromRoleAsync(foundUser,roleName);
		}

    }
}
