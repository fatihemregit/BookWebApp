using AutoMapper;
using Business.Abstracts.Auth;
using Data;
using Data.Abstracts.Auth;
using Entity;
using Entity.Auth;
using Entity.Exceptions;
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
    public class AuthUserService : IAuthUserService
    {
        private readonly IAuthUserRepository _userRepository;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IMapper _mapper;

        public AuthUserService(IAuthUserRepository userRepository, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _signInManager = signInManager;
        }




        public async Task<IdentityResult> CreateAsync(IAuthUserServiceCreateAsync user, string password)
        {
            IdentityResult result = await _userRepository.CreateAsync(_mapper.Map<IAuthUserRepositoryCreateAsync>(user), password);
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
            if (userName is null)
            {
                return null;
            }
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
            bool isInRoleAsync = await _userRepository.IsInRoleAsync(_mapper.Map<IAuthUserRepositoryIsInRoleAsync>(user), roleName);
            return isInRoleAsync;
        }

        public async Task AddToRoleAsync(IAuthUserServiceAddToRoleAsync user, string roleName)
        {
            Console.WriteLine("AuthUserService/AddToRoleAsync metodu çalıştı");
            await _userRepository.AddToRoleAsync(_mapper.Map<IAuthUserRepositoryAddToRoleAsync>(user), roleName);
        }

        public async Task RemoveFromRoleAsync(IAuthUserServiceRemoveFromRoleAsync user, string roleName)
        {
            await _userRepository.RemoveFromRoleAsync(_mapper.Map<IAuthUserRepositoryRemoveFromRoleAsync>(user), roleName);
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




        public async Task<Exception> SignIn(IAuthUserServiceSignIn user)
        {
            IdentityResult result = await _userRepository.CreateAsync(_mapper.Map<IAuthUserRepositoryCreateAsync>(user), user.Sifre);
            if (result.Succeeded)
            {
                //return IAuthUserServiceSignInSucceeded
                return new  IAuthUserServiceSignInSucceeded("User Sign in is sucess");
            }
            else
            {
                //AuthUserServiceSignInNotSucceeded
                return new IAuthUserServiceSignInNotSucceeded("User Sign in is not sucess",result.Errors);
            }
        }

        public async Task<Exception> Login(IAuthUserServiceLogin user)
        {
            //useri bulalım
            AppUser? foundUser = await _userRepository.FindByEmailAsyncAndReturnAppUser(user.Email);
            if (foundUser is null)
            {
                return new IAuthUserServiceLoginNotSucceeded("user not found");
            }
            //İlgili kullanıcıya dair önceden oluşturulmuş bir Cookie varsa siliyoruz.
            await _signInManager.SignOutAsync();
            //giriş yapalım
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(foundUser, user.Password, user.Persistent, false);
            if (result.Succeeded)
            {
                return new IAuthUserServiceLoginSucceeded("PasswordSignInAsync result Succeeded");
            }
            return new IAuthUserServiceLoginNotSucceeded("PasswordSignInAsync result not Succeeded");

        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Exception> DeleteUser(string userName)
        {
            //kullanıcıyı bulalım
            IAuthUserRepositoryFindByNameAsync? foundUser = await _userRepository.FindByNameAsync(userName);
            if (foundUser is null)
            {
                return new IAuthUserServiceDeleteUserNotSucceeded("User not found");
            }
            //kullanıcıyı silelim
            IdentityResult? result = await _userRepository.DeleteAsync(_mapper.Map<IAuthUserRepositoryDeleteAsync>(foundUser));
            if (result.Succeeded)
            {
                return new IAuthUserServiceDeleteUserSucceeded("DeleteAsync result succeeded");
            }
            return new IAuthUserServiceDeleteUserNotSucceeded("DeleteAsync result not succeeded");


        }

        public async Task<bool> checkUserIsLogin(string userName)
        {
            Console.WriteLine("checkUserIsLogin fonksiyonu çalıştı");
            if (userName is null)
            {
                Console.WriteLine("userName is null");
                return false;
            }
            


            IAuthUserRepositoryFindByNameAsync? findByNameAsync = await _userRepository.FindByNameAsync(userName);
            if (findByNameAsync is null)
            {
                Console.WriteLine("findByNameAsync is null");
                return false;
            }
           
            IAuthUserRepositoryFindByEmailAsync? findByEmailAsync = await _userRepository.FindByEmailAsync(findByNameAsync.Email);
            if (findByEmailAsync is null)
            {
                Console.WriteLine("findByEmailAsync is null");

                return false;
            }
            AppUser foundAppUser = _mapper.Map<AppUser>(findByEmailAsync);
            if (foundAppUser is not null)
            {
                return true;
            }
            return false;
        }
    }
}
