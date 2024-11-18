﻿using AutoMapper;
using BookWebApp.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApp.Controllers
{
    public class UserController : Controller
    {


        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;


        /* username:email:password
         *  admin:admin@admin.com:&dY3#cQjA8d!BAZo9vYLidDg
         *  
         */

        public UserController(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<AppUserViewModel> appUserViewModels = new List<AppUserViewModel>();
            List<AppUser> users = _userManager.Users.ToList();

			foreach (AppUser appUser in users)
            {
                AppUserViewModel appUserViewModel = new AppUserViewModel();
                appUserViewModel.UserName = appUser.UserName;
                appUserViewModel.Email = appUser.Email;
                appUserViewModel.Roles = await _userManager.GetRolesAsync(appUser);
				appUserViewModels.Add(appUserViewModel);

			}

            return View(appUserViewModels);
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserViewModel appUserViewModel)
        {
            if (ModelState.IsValid)
            {

                AppUser appUser = _mapper.Map<AppUser>(appUserViewModel);

                IdentityResult result = await _userManager.CreateAsync(appUser, appUserViewModel.Sifre);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                }

            }
            return View();
        }


        [HttpGet]
        public IActionResult Login(string returnUrl)
        {

            TempData["returnUrl"] = returnUrl;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            { 
                AppUser founduser = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (founduser != null)
                {
                    //İlgili kullanıcıya dair önceden oluşturulmuş bir Cookie varsa siliyoruz.
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(founduser, loginViewModel.Password, loginViewModel.Persistent, false);
                    if (result.Succeeded)
                        return Redirect(TempData["returnUrl"].ToString());
                }
                else
                {
                    ModelState.AddModelError("NotUser", "Böyle bir kullanıcı bulunmamaktadır.");
                    ModelState.AddModelError("NotUser2", "E-posta veya şifre yanlış.");
                }
            }

            return View(loginViewModel);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "DeleteUser")]
        public async Task<IActionResult> DeleteUser(string UserName)
        {
            Console.WriteLine("deleting users");
            AppUser founduser = await _userManager.FindByNameAsync(UserName);
            if (founduser is null)
            {
                return NotFound();
            }
            Console.WriteLine("kullanıcı bulundu");
            Console.WriteLine("kullanıcı siliniyor");
            await _userManager.DeleteAsync(founduser);
            Console.WriteLine("kullanıcı silindi");
            return RedirectToAction("Index", "User");
        }

    }
}