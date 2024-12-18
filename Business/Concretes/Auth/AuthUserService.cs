﻿using AutoMapper;
using Business.Abstracts.Auth;
using Data;
using Data.Abstracts.Auth;
using Entity;
using Entity.Auth;
using Entity.Exceptions;
using Entity.Exceptions.IAuthUserService;
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


        //NEW CODES
        public async Task<List<IAuthUserServiceGetAllUsersAsync>?> GetAllUsersAsync()
        {
            List<IAuthUserRepositoryGetAllUsersAsync>? getAllUsersAsync = await _userRepository.GetAllUsersAsync();
            if (getAllUsersAsync is null)
            {
                return null;
            }
            return _mapper.Map<List<IAuthUserServiceGetAllUsersAsync>>(getAllUsersAsync);


        }

        public async Task<Exception> SignIn(IAuthUserServiceSignIn user)
        {
            if (user is null)
            {
                return new IAuthUserServiceSignInNotSucceeded("user paramater is null");
            }
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
            if (user is null)
            {
                return new IAuthUserServiceLoginNotSucceeded("user parameters is null");
            }

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
            if (userName is null)
            { 
                return new IAuthUserServiceDeleteUserNotSucceeded("userName parameter is null");
            }

            //kullanıcıyı bulalım
            IAuthUserRepositoryFindByNameAsync? foundUser = await _userRepository.FindByNameAsync(userName);
            if (foundUser is null)
            {
                return new IAuthUserServiceDeleteUserNotSucceeded("User not found");
            }
            //kullanıcıyı silelim
            IdentityResult result = await _userRepository.DeleteAsync(_mapper.Map<IAuthUserRepositoryDeleteAsync>(foundUser));
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

        public async Task<IAuthUserServiceFindLocalUserwithUserName?> findLocalUserwithUserName(string userName)
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
            return _mapper.Map<IAuthUserServiceFindLocalUserwithUserName>(findByNameAsync);
           
        }
        public async Task<Dictionary<string,bool>> checkRoleswithLocalUserName(string userName,List<string> roleNames)
        {
            IAuthUserServiceFindLocalUserwithUserName? localUser = await findLocalUserwithUserName(userName);
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            if (localUser is null) {
                foreach (string role in roleNames)
                {
                    result.Add(role,false);
                }
                return result;
            }
            foreach (string role in roleNames)
            {
                result.Add(role, await _userRepository.IsInRoleAsync(_mapper.Map<IAuthUserRepositoryIsInRoleAsync>(localUser),role));
            }
            return result;
        }

    }
}
