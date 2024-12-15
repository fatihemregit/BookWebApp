using AutoMapper;
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
            //email ile userı bulalım
            IAuthUserServiceFindByEmailAsync? foundUser = await _userService.FindByEmailAsync(userEmail);
            //foundUser null check
            if (foundUser is null)
            {
                return BadRequest("kullanıcı bulunamadı");
            }
            //sistemdeki tüm rolleri alalım
            List<IAuthRoleServiceGetAllRolesAsync>? allRoles = await _roleService.GetAllRolesAsync();
            //sistemde hiç rol yoksa rol atamayı yapamayız(nası yapcan rol yok??)
            if (allRoles is null)
            {
                return BadRequest("Sistemde kayıtlı rol olmadan rol atayamazsınız");
            }
            //bulunan kullanıcının tüm rollerini alalım
            IList<string>? foundUserRoles = await _userService.GetRolesAsync(_mapper.Map<IAuthUserServiceGetRolesAsync>(foundUser));
            //bulunan kullanıcının rolleri ile sistemdeki tüm rolleri karşılaştırarak viewde tikli olup olmamasını belirleyelim
            //eğer kullanıcının hiç rolü yoksa herhangi bir karşılaştırma yapmaya gerek yok
            List<SetRoleForUserViewModel> setRoleForUserViewModels = new List<SetRoleForUserViewModel>();
            if (foundUserRoles is null)
            {
                //eğer kullanıcının hiç rolü yoksa herhangi bir karşılaştırma yapmaya gerek yok
                allRoles.ForEach(r =>
                {
                    setRoleForUserViewModels.Add(new SetRoleForUserViewModel() { RoleName = r.Name, State = false });
                });
            }
            else
            {
                foreach (IAuthRoleServiceGetAllRolesAsync item in allRoles)
                {
                    setRoleForUserViewModels.Add(new SetRoleForUserViewModel() { RoleName = item.Name, State = foundUserRoles.Any(s => s == item.Name) });
                }
            }
            TempData["userEmail"] = userEmail;
            return View(setRoleForUserViewModels);

        }
        [Authorize(Roles = "role_set")]
        [HttpPost]
        public async Task<IActionResult> SetRoleForUser(List<SetRoleForUserViewModel> setRoleViewModels, string userEmail)
        {
            Console.WriteLine("========================================================================");
            Console.WriteLine("RoleController/SetRoleForUser Post metodu çalıştı");
            //mail ile kullanıcıyı bulalım
            IAuthUserServiceFindByEmailAsync? foundUserbyEmail = await _userService.FindByEmailAsync(userEmail);
            if (foundUserbyEmail is null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }
            //viewmodel in içine girelim ve tik durumuna göre rol atayıp silelim
            //viewmodel in içine girelim
            foreach (SetRoleForUserViewModel item in setRoleViewModels)
            {
                //tik durumuna göre rol atayıp silelim
                //rol atama
                if (item.State)
                {
                    await _userService.AddToRoleAsync(_mapper.Map<IAuthUserServiceAddToRoleAsync>(foundUserbyEmail), item.RoleName);
                }
                else
                {
                    await _userService.RemoveFromRoleAsync(_mapper.Map<IAuthUserServiceRemoveFromRoleAsync>(foundUserbyEmail), item.RoleName);
                }
            }
            //burada kaldım
            //roller ayarlandı ancak etkili olması için giriş çıkış yapılması gerekli(burası biraz sıkıntılı,bakılması lazım)
            //hali hazırdaki kullanıcıyı bulalım
            //şuan bu kod çok kötü bir kod düzenlenemesi lazım
            string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            IAuthUserServiceFindByEmailAsync localUserWithUserId = _mapper.Map<IAuthUserServiceFindByEmailAsync>((await _userService.GetAllUsersAsync()).Where(u => u.Id.ToString() == userId).SingleOrDefault());
            if (localUserWithUserId == foundUserbyEmail)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(_mapper.Map<AppUser>(foundUserbyEmail), true);
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
