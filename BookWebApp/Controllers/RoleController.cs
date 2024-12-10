using AutoMapper;
using Entity.Auth;
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

        private readonly IMapper _mapper;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
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
            if (ModelState.IsValid)
            {
                if (createRoleViewModel == null)
                {
                    return BadRequest();
                }
                IdentityResult result = await _roleManager.CreateAsync(_mapper.Map<AppRole>(createRoleViewModel));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                }
            }
            return View(createRoleViewModel);

        }

        //[Authorize(Roles = "role_delete")]
        //[HttpGet]
        //public IActionResult DeleteRole()
        //{
        //    DeleteRoleViewModel deleteRoleViewModel = new DeleteRoleViewModel();
        //    deleteRoleViewModel.RoleNames = _roleManager.Roles.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList();
        //    return View(deleteRoleViewModel);
        //}

        //[Authorize(Roles = "role_delete")]
        //[HttpPost("DeleteRole")]
        //public async Task<IActionResult> DeleteRolePost(DeleteRoleViewModel deleteRoleViewModel)
        //{
        //    //deleting Role
        //    //find a role with id which comes from deleteRoleViewModel
        //    AppRole foundRole = await _roleManager.FindByIdAsync(deleteRoleViewModel.SelectedRoleId);
        //    if (foundRole == null)
        //    {
        //        return BadRequest("Böyle bir Rol Yok!");
        //    }
        //    Console.WriteLine("========================================================");
        //    Console.WriteLine("delete role post method for each");
        //    Console.WriteLine();
        //    IList<AppUser> usersWithFoundRoleName = await _userManager.GetUsersInRoleAsync(foundRole.Name);
        //    if (usersWithFoundRoleName.Count > 0) 
        //    {
        //        return BadRequest("Bu Rol Aktif Olarak Kullanılıyor!");
        //    }

        //    await _roleManager.DeleteAsync(foundRole);
        //    return RedirectToAction("Index", "User");
        //}



        [Authorize(Roles = "role_set")]
        [HttpGet]
        public async Task<IActionResult> SetRoleForUser(string userEmail = null)
        {
            if (userEmail is null)
            {
                return BadRequest();
            }

            AppUser foundUser = await _userManager.FindByEmailAsync(userEmail);

            if (foundUser == null)
            {
                return NotFound();
            }

            IList<string> foundUserRoles = await _userManager.GetRolesAsync(foundUser);
            List<SetRoleForUserViewModel> setRoleViewModels = new List<SetRoleForUserViewModel>();
            //get All Roles in db
            IQueryable<AppRole> allRoles = _roleManager.Roles;


            foreach (AppRole role in allRoles)
            {
                SetRoleForUserViewModel setRoleViewModel = new SetRoleForUserViewModel();
                setRoleViewModel.RoleName = role.Name;
                setRoleViewModel.State = foundUserRoles.Contains(role.Name);
                setRoleViewModels.Add(setRoleViewModel);
            }
            TempData["userEmail"] = userEmail;

            return View(setRoleViewModels);
        }
        [Authorize(Roles = "role_set")]
        [HttpPost]
        public async Task<IActionResult> SetRoleForUser(List<SetRoleForUserViewModel> setRoleViewModels, string userEmail)
        {
            Console.WriteLine("========================================================================");

            Console.WriteLine("userEmail is " + userEmail);

            AppUser foundUserbyEmail = await _userManager.FindByEmailAsync(userEmail);

            if (foundUserbyEmail is null)
            {
                return NotFound("Kullanıcı bulunamadı");

            }

            foreach (SetRoleForUserViewModel setRoleViewModel in setRoleViewModels)
            {
                if (setRoleViewModel.State)
                {
                    await _userManager.AddToRoleAsync(foundUserbyEmail, setRoleViewModel.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(foundUserbyEmail, setRoleViewModel.RoleName);
                }
            }
            //roller ayarlandı ancak etkili olması için giriş çıkış yapılması gerekli(burası biraz sıkıntılı,bakılması lazım)
            AppUser user = await _userManager.GetUserAsync(User);
            if (user == foundUserbyEmail)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(foundUserbyEmail, true);
            }
            else
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "User");
        }




    }
    //end
}
