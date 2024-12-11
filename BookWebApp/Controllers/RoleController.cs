using AutoMapper;
using Business.Abstracts.Auth;
using Entity.Auth;
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
				IdentityResult result = await _roleService.CreateAsync(_mapper.Map<IAuthRoleServiceCreateAsync>(createRoleViewModel));

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

		[Authorize(Roles = "role_delete")]
		[HttpGet]
		public async Task<IActionResult> DeleteRole()
		{
			DeleteRoleViewModel deleteRoleViewModel = new DeleteRoleViewModel();
			List<IAuthRoleServiceGetAllRolesAsync>? getAllRolesAsync = await _roleService.GetAllRolesAsync();
			if (getAllRolesAsync is null)
			{
				return BadRequest("Sistemde kayıtlı rol olmadan rol silemezsiniz");
			}
			List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> RoleNames = (getAllRolesAsync).Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = r.Id.ToString(),Text = r.Name}).ToList();
			ViewBag.RoleNames = RoleNames;
			return View(deleteRoleViewModel);
		}

		[Authorize(Roles = "role_delete")]
		[HttpPost("DeleteRole")]
		public async Task<IActionResult> DeleteRolePost(DeleteRoleViewModel deleteRoleViewModel)
		{

			//rolü id ile  bulalım 
			IAuthRoleServiceFindByIdAsync? foundrolewithId = await _roleService.FindByIdAsync(deleteRoleViewModel.SelectedRoleId);
			if (foundrolewithId is null)
			{
				return BadRequest("rol bulunamadı");
			}
			//bu rolün aktif olarak kullanılıp kullanılmadığını öğrenelim
			List<IAuthUserServiceGetUsersInRoleAsync>? getUsersInRoleAsync = await _userService.GetUsersInRoleAsync(foundrolewithId.Name);
			if (getUsersInRoleAsync is  not null)
			{
				//rol aktif olarak kullanılıyor
				return BadRequest("Bu Rol Aktif Olarak Kullanılıyor!");
			}
			//rol aktif olarak kullanılmıyor
			//rol silme 
			await _roleService.DeleteAsync(_mapper.Map<IAuthRoleServiceDeleteAsync>(foundrolewithId));
			return RedirectToAction("Index", "User");
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
			if (foundUser is null)
			{
				//eğer kullanıcının hiç rolü yoksa herhangi bir karşılaştırma yapmaya gerek yok
				allRoles.ForEach(r =>
				{
					setRoleForUserViewModels.Add(new SetRoleForUserViewModel() { RoleName = r.Name,State = false});
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

			Console.WriteLine("userEmail is " + userEmail);
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
					await _userService.AddToRoleAsync(_mapper.Map<IAuthUserServiceAddToRoleAsync>(foundUserbyEmail),item.RoleName);
				}
				else
				{
					await _userService.RemoveFromRoleAsync(_mapper.Map<IAuthUserServiceRemoveFromRoleAsync>(foundUserbyEmail),item.RoleName);
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
