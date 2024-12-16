using AutoMapper;
using Business.Abstracts.Auth;
using Data.Abstracts.Auth;
using Entity.Auth;
using Entity.Exceptions;
using Entity.IAuthRoleRepository;
using Entity.IAuthRoleService;
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
    public class AuthRoleService : IAuthRoleService
    {

        private readonly IAuthRoleRepository _roleRepository;

        private readonly IAuthUserRepository _userRepository;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IMapper _mapper;

        public AuthRoleService(IAuthRoleRepository RoleRepository, IMapper mapper, IAuthUserRepository userRepository, SignInManager<AppUser> signInManager)
        {
            _roleRepository = RoleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _signInManager = signInManager;
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

        public async Task<Exception> SetRoleForUserGet(string userEmail)
        {
            if (userEmail is null)
            {
                return new IAuthRoleServiceSetRoleForUserGetNotSucceeded("useremail is null");
            }

            //email ile userı bulalım
            IAuthUserRepositoryFindByEmailAsync? foundUserwithEmail = await  _userRepository.FindByEmailAsync(userEmail);
            
            if (foundUserwithEmail is null)
            {
                //NotSucceeded dönelim
                return new IAuthRoleServiceSetRoleForUserGetNotSucceeded("user not found");
            }
            //sistemdeki tüm rolleri alalım
            List<IAuthRoleRepositoryGetAllRolesAsync>? rolesinrepository = await _roleRepository.GetAllRolesAsync();
            //sistemde hiç rol yoksa rol atamayı yapamayız
            if (rolesinrepository is null)
            {
                //NotSucceeded dönelim
                return new IAuthRoleServiceSetRoleForUserGetNotSucceeded("does not have any roles in system");
            }
            //bulunan kullanıcının tüm rollerini alalım
            IList<string>? foundUserRoles = await _userRepository.GetRolesAsync(_mapper.Map<IAuthUserRepositoryGetRolesAsync>(foundUserwithEmail));
            //bulunan kullanıcının rolleri ile sistemdeki tüm rolleri karşılaştırarak viewde tikli olup olmamasını belirleyelim
            List<IAuthRoleServiceSetRoleForUserGet> setroles = new List<IAuthRoleServiceSetRoleForUserGet>();
            bool foundUserRolesNullState = foundUserRoles is null;
            foreach (IAuthRoleRepositoryGetAllRolesAsync role in rolesinrepository)
            {
                setroles.Add(new IAuthRoleServiceSetRoleForUserGet { RoleName = role.Name, State = foundUserRolesNullState ? false: foundUserRoles.Any(s => s == role.Name) });
            }
            //Succeeded dönelim
            return new IAuthRoleServiceSetRoleForUserGetSucceeded("SetRoleForUserGet is succeeded", setroles);

        }

        public async Task<Exception> SetRoleForUserPost(List<IAuthRoleServiceSetRoleForUserPost> roles, string userEmail,string localUserName)
        {
            //kullanıcıyı bulalım
            IAuthUserRepositoryFindByEmailAsync? foundUser = await _userRepository.FindByEmailAsync(userEmail);
            //kullanıcı yoksa NotSucceeded dönelim
            if (foundUser is null)
            {
                return new IAuthRoleServiceSetRoleForUserPostNotSucceeded("user not found");
            }
            //role listesine girip rol atama ve silme işlemlerini yapalım
            foreach (IAuthRoleServiceSetRoleForUserPost role in roles)
            {
                if (role.State)
                {
                    //rol ata
                    await _userRepository.AddToRoleAsync(_mapper.Map<IAuthUserRepositoryAddToRoleAsync>(foundUser),role.RoleName);
                }
                else
                {
                    //rol sil
                    await _userRepository.RemoveFromRoleAsync(_mapper.Map<IAuthUserRepositoryRemoveFromRoleAsync>(foundUser),role.RoleName);
                }
            }
            //rol atama ve silme işlemlerinin geçerli olması için gir çık yapılması gerekli
            //eğer local user ile rol işlemi yapılan kullanıcı aynı ise gir çık yapılsın fakat aynı değil ise herhangi bir şey yapılmasın
            if (foundUser.UserName == localUserName)
            {
                //local user ile rol işlemi yapılan kullanıcı aynı
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(_mapper.Map<AppUser>(foundUser), true);
            }
            //local user ile rol işlemi yapılan kullanıcı aynı değil
            //işlemler başarıyla tamamlandı Succeeded dönelim
            return new IAuthRoleServiceSetRoleForUserPostSucceeded("SetRoleForUserPost is Succeeded");
        }
    }
}
