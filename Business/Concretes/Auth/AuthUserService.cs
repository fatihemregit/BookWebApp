using AutoMapper;
using Business.Abstracts.Auth;
using Data;
using Data.Abstracts.Auth;
using Entity.IAuthUserRepository;
using Entity.IAuthUserService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes.Auth
{
	public class AuthUserService:IAuthUserService
	{
		private readonly IAuthUserRepository _userRepository;

		private readonly IMapper _mapper;

		public AuthUserService(IAuthUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		

		public async Task<IdentityResult> CreateAsync(IAuthUserServiceCreateAsync user, string password)
		{
			IdentityResult result = await _userRepository.CreateAsync(_mapper.Map<IAuthUserRepositoryCreateAsync>(user),password);
			return result;
		}

		public async Task<IdentityResult> DeleteAsync(IAuthUserServiceDeleteAsync user)
		{
			IdentityResult result = await _userRepository.DeleteAsync(_mapper.Map<IAuthUserRepositoryDeleteAsync>(user));
			return result;
		}

		public async Task<IAuthUserServiceFindByEmailAsync?> FindByEmailAsync(string email)
		{
			IAuthUserRepositoryFindByEmailAsync? findByEmailAsync = await _userRepository.FindByEmailAsync(email);
			if (findByEmailAsync is null)
			{
				return null;
			}
			return _mapper.Map<IAuthUserServiceFindByEmailAsync>(findByEmailAsync);
		}

		public async Task<IAuthUserServiceFindByNameAsync?> FindByNameAsync(string userName)
		{
			IAuthUserRepositoryFindByNameAsync? findByNameAsync = await _userRepository.FindByNameAsync(userName);
			if (findByNameAsync is null)
			{
				return null;
			}
			return _mapper.Map<IAuthUserServiceFindByNameAsync>(findByNameAsync);

		}

		public async Task<List<IAuthUserServiceGetAllUsersAsync>?> GetAllUsersAsync()
		{
			List<IAuthUserRepositoryGetAllUsersAsync>? getAllUsersAsync = await _userRepository.GetAllUsersAsync();
			if (getAllUsersAsync is null)
			{ 
				return null;
			}
			return _mapper.Map<List<IAuthUserServiceGetAllUsersAsync>>(getAllUsersAsync);


		}

		public async Task<IList<string>?> GetRolesAsync(IAuthUserServiceGetRolesAsync user)
		{
			IList<string>? getRolesAsync = await _userRepository.GetRolesAsync(_mapper.Map<IAuthUserRepositoryGetRolesAsync>(user));
			if (getRolesAsync is null)
			{
				return null;
			}
			return getRolesAsync;

		}

		public async Task<bool> IsInRoleAsync(IAuthUserServiceIsInRoleAsync user, string roleName)
		{
			bool isInRoleAsync = await _userRepository.IsInRoleAsync(_mapper.Map<IAuthUserRepositoryIsInRoleAsync>(user),roleName);
			return isInRoleAsync;
		}

		public async Task AddToRoleAsync(IAuthUserServiceAddToRoleAsync user, string roleName)
		{
			await _userRepository.AddToRoleAsync(_mapper.Map<IAuthUserRepositoryAddToRoleAsync>(user), roleName);
		}

		public async Task RemoveFromRoleAsync(IAuthUserServiceRemoveFromRoleAsync user, string roleName)
		{
			 await _userRepository.RemoveFromRoleAsync(_mapper.Map<IAuthUserRepositoryRemoveFromRoleAsync>(user),roleName);
		}

        public async Task<List<IAuthUserServiceGetUsersInRoleAsync>?> GetUsersInRoleAsync(string roleName)
        {
			List<IAuthUserRepositoryGetUsersInRoleAsync>? getUsersInRole = await _userRepository.GetUsersInRoleAsync(roleName);
			if (getUsersInRole is null)
			{ 
				return null;
			}
			return _mapper.Map<List<IAuthUserServiceGetUsersInRoleAsync>>(getUsersInRole);

        }
    }
}
