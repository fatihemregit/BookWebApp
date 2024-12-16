﻿using AutoMapper;
using Business.Abstracts.Auth;
using Entity.Auth;
using Entity.Exceptions;
using Entity.IAuthRoleService;
using Entity.IAuthUserRepository;
using Entity.IAuthUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookWebApp.Controllers
{

    //bunun authorize işlemini en son yap.çünkü rol authorize işlemi için kritik bir controller
    public class RoleController : Controller
    {

        private readonly RoleManager<AppRole> _roleManager;

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IAuthRoleService _roleService;

        private readonly IAuthUserService _userService;

        private readonly IMapper _mapper;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager, IAuthRoleService roleService, IAuthUserService userService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _roleService = roleService;
            _userService = userService;
        }



        [Authorize(Roles = "role_index")]
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "role_create")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [Authorize(Roles = "role_create")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            //tamam
            if (ModelState.IsValid)
            {
                if (createRoleViewModel == null)
                {
                    return BadRequest();
                }
                Exception result = await _roleService.CreateRolePost(_mapper.Map<IAuthRoleServiceCreateRolePost>(createRoleViewModel));
                if (result is IAuthRoleServiceCreateRoleSucceeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    ((IAuthRoleServiceCreateRoleNotSucceeded)result).Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                }
            }
            return View(createRoleViewModel);

        }

        [Authorize(Roles = "role_delete")]
        [HttpGet]
        public async Task<IActionResult> DeleteRole()
        {
            Exception result = await _roleService.DeleteRoleGet();
            if (result is IAuthRoleServiceDeleteRoleGetNotSucceeded)
            {
                return BadRequest("Sistemde kayıtlı rol olmadan rol silemezsiniz");
            }
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> RoleNames = ((IAuthRoleServiceDeleteRoleGetSucceeded)result).Roles.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList();
            ViewBag.RoleNames = RoleNames;
            return View();
        }

        [Authorize(Roles = "role_delete")]
        [HttpPost("DeleteRole")]
        public async Task<IActionResult> DeleteRolePost(DeleteRoleViewModel deleteRoleViewModel)
        {


            Exception result = await _roleService.DeleteRolePost(_mapper.Map<IAuthRoleServiceDeleteRolePost>(deleteRoleViewModel));
            //burası daha güzel yazılabilir 
            if ((result is IAuthRoleServiceDeleteRolePostNotSucceeded) && (result.Message == "role not found"))
            {
                return BadRequest("rol bulunamadı");
            }
            else if ((result is IAuthRoleServiceDeleteRolePostNotSucceeded) && (result.Message == "role is active"))
            {
                return BadRequest("Bu Rol Aktif Olarak Kullanılıyor!");
            }
            else if ((result is IAuthRoleServiceDeleteRolePostNotSucceeded) && (result.Message == "DeleteAsync is not Succeeded"))
            {
                return BadRequest("DeleteAsync is not Succeeded");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }



        [Authorize(Roles = "role_set")]
        [HttpGet]
        public async Task<IActionResult> SetRoleForUser(string userEmail = null)
        {
            Exception result = await _roleService.SetRoleForUserGet(userEmail);
            if (result is IAuthRoleServiceSetRoleForUserGetNotSucceeded)
            {
                return BadRequest(result.Message);
            }
            else
            {
                TempData["userEmail"] = userEmail;
                List<IAuthRoleServiceSetRoleForUserGet> setroles = ((IAuthRoleServiceSetRoleForUserGetSucceeded)result).setroles;
                List<SetRoleForUserViewModel> setRoleForUserViewModels = _mapper.Map<List<SetRoleForUserViewModel>>(setroles);
                return View(setRoleForUserViewModels);
            }

        }
        [Authorize(Roles = "role_set")]
        [HttpPost]
        public async Task<IActionResult> SetRoleForUser(List<SetRoleForUserViewModel> setRoleViewModels, string userEmail)
        {
            Exception result = await _roleService.SetRoleForUserPost(_mapper.Map<List<IAuthRoleServiceSetRoleForUserPost>>(setRoleViewModels),userEmail,User.Identity.Name);
            if (result is IAuthRoleServiceSetRoleForUserPostSucceeded)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return BadRequest(result.Message);
            }
        }




    }
    //end
}
