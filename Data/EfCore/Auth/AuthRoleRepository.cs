using Data.Abstracts.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.IAuthRoleRepository;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Entity.Auth;
using Microsoft.EntityFrameworkCore;

namespace Data.EfCore.Auth
{
	public class AuthRoleRepository : IAuthRoleRepository
	{


		private readonly RoleManager<AppRole> _roleManager;

		private readonly IMapper _mapper;


		public AuthRoleRepository(RoleManager<AppRole> roleManager, IMapper mapper)
		{
			_roleManager = roleManager;
			_mapper = mapper;
		}

		public async Task<IdentityResult> CreateAsync(IAuthRoleRepositoryCreateAsync role)
		{
			IdentityResult identityResult = await _roleManager.CreateAsync(_mapper.Map<AppRole>(role));
			return identityResult;
		}

		public async Task<IdentityResult> DeleteAsync(IAuthRoleRepositoryDeleteAsync role)
		{
			AppRole appRole = await _roleManager.FindByIdAsync(role.Id.ToString());
			IdentityResult result = await _roleManager.DeleteAsync(appRole);
			return result;
		}

		public async Task<IAuthRoleRepositoryFindByIdAsync?> FindByIdAsync(string roleId)
		{
			AppRole? foundAppRole = await _roleManager.FindByIdAsync(roleId);
			if (foundAppRole is null)
			{ 
				return null;
			}
			return _mapper.Map<IAuthRoleRepositoryFindByIdAsync>(foundAppRole);

		}

		public async Task<List<IAuthRoleRepositoryGetAllRolesAsync>?> GetAllRolesAsync()
		{
			//Roles null sa zaten burada null hatası almaz mıyız?
			List<AppRole> allAppRoles = await _roleManager.Roles.ToListAsync();

			if (allAppRoles is null)
			{
				return null;
			}
			return _mapper.Map<List<IAuthRoleRepositoryGetAllRolesAsync>>(allAppRoles);

		}
	}
}
