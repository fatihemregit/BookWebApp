using AutoMapper;
using Business.Abstracts.Auth;
using Data.Abstracts.Auth;
using Entity.Exceptions;
using Entity.IAuthRoleRepository;
using Entity.IAuthRoleService;
using Entity.IAuthUserRepository;
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

        private readonly IAuthUserRepository _userRepository;

        private readonly IMapper _mapper;

        public AuthRoleService(IAuthRoleRepository RoleRepository, IMapper mapper, IAuthUserRepository userRepository)
        {
            _roleRepository = RoleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
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

        public async Task<Exception> CreateRolePost(IAuthRoleServiceCreateRolePost role)
        {
            //parameter null check(daha sonra ekle)
            IdentityResult result = await _roleRepository.CreateAsync(_mapper.Map<IAuthRoleRepositoryCreateAsync>(role));
            if (result.Succeeded)
            {
                return new IAuthRoleServiceCreateRoleSucceeded("CreateRole is succeeded");

            }
            return new IAuthRoleServiceCreateRoleNotSucceeded("CreateRole is not succeeded", result.Errors);


        }

        public async Task<Exception> DeleteRoleGet()
        {
            //sistemdeki rolleri çek
            //sistemdeki rolleri çekerken null geliyorsa NotSucceeded dön,gelmiyorsa Succeeded dön
            List<IAuthRoleRepositoryGetAllRolesAsync>? rolesinrepository = await _roleRepository.GetAllRolesAsync();
            if (rolesinrepository is null)
            {
                return new IAuthRoleServiceDeleteRoleGetNotSucceeded("does not have any roles in system");
            }

            List<IAuthRoleServiceGetAllRolesAsync> roles = _mapper.Map<List<IAuthRoleServiceGetAllRolesAsync>>(rolesinrepository);
            return new IAuthRoleServiceDeleteRoleGetSucceeded("GetAllRolesAsync is Succeeded", roles);
        }

        public async Task<Exception> DeleteRolePost(IAuthRoleServiceDeleteRolePost role)
        {
            //rolü id ile  bulalım
            IAuthRoleRepositoryFindByIdAsync? foundRoleWithId = await _roleRepository.FindByIdAsync(role.SelectedRoleId);
            if (foundRoleWithId is null)
            {
                //rol yoksa NotSucceeded dönelim
                return new IAuthRoleServiceDeleteRolePostNotSucceeded("role not found");
            }
            //bu rolün aktif olarak kullanılıp kullanılmadığını öğrenelim
            List<IAuthUserRepositoryGetUsersInRoleAsync>? getUsersInRoleAsync = await _userRepository.GetUsersInRoleAsync(foundRoleWithId.Name);
            if (getUsersInRoleAsync is not null)
            {
                //rol aktif olarak kullanılıyorsa NotSucceeded dönelim
                return new IAuthRoleServiceDeleteRolePostNotSucceeded("role is active");
            }
            //rol aktif olarak kullanılmıyor
            //rol silme 
            IdentityResult result = await _roleRepository.DeleteAsync(_mapper.Map<IAuthRoleRepositoryDeleteAsync>(foundRoleWithId));
            if (result.Succeeded)
            {
                //silme başarılı,Succeeded dönelim
                return new IAuthRoleServiceDeleteRolePostSucceeded("DeleteAsync is Succeeded");
            }
            //silme başarısız,NotSucceeded dönelim
            return new IAuthRoleServiceDeleteRolePostNotSucceeded("DeleteAsync is not Succeeded");
        }
    }
}
