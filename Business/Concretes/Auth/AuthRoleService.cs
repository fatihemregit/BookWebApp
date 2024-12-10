using AutoMapper;
using Business.Abstracts.Auth;
using Data.Abstracts.Auth;
using Entity.IAuthRoleRepository;
using Entity.IAuthRoleService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes.Auth
{
	public class AuthRoleService : IAuthRoleService
	{

		private readonly IAuthRoleRepository _roleRepository;

		private readonly IMapper _mapper;

		public AuthRoleService(IAuthRoleRepository authRoleRepository, IMapper mapper)
		{
			_roleRepository = authRoleRepository;
			_mapper = mapper;
		}


		public async Task<IdentityResult> CreateAsync(IAuthRoleServiceCreateAsync role)
		{
			IdentityResult identityResult = await _roleRepository.CreateAsync(_mapper.Map<IAuthRoleRepositoryCreateAsync>(role));
			return identityResult;
		}

		public async Task DeleteAsync(IAuthRoleServiceDeleteAsync role)
		{
			await _roleRepository.DeleteAsync(_mapper.Map<IAuthRoleRepositoryDeleteAsync>(role));

		}

		public async Task<IAuthRoleServiceFindByIdAsync?> FindByIdAsync(string roleId)
		{
			IAuthRoleRepositoryFindByIdAsync? findByIdAsync = await _roleRepository.FindByIdAsync(roleId);
			if (findByIdAsync == null)
			{ 
				return null;
			}
			return _mapper.Map<IAuthRoleServiceFindByIdAsync>(findByIdAsync);

		}

		public async Task<List<IAuthRoleServiceGetAllRolesAsync>?> GetAllRolesAsync()
		{
			List<IAuthRoleRepositoryGetAllRolesAsync>? getAllRolesAsync = await _roleRepository.GetAllRolesAsync();
			if (getAllRolesAsync is null)
			{
				return null;
			}
			return _mapper.Map<List<IAuthRoleServiceGetAllRolesAsync>>(getAllRolesAsync);

		}
	}
}
