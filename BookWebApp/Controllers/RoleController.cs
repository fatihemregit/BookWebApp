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
			//AppUser user = await _userService.;
			//if (user == foundUserbyEmail)
			//{
			//	await _signInManager.SignOutAsync();
			//	await _signInManager.SignInAsync(foundUserbyEmail, true);
			//}
			//else
			//{
			//	await _signInManager.SignOutAsync();
			//}
			return RedirectToAction("Index", "User");



			//kullanıcıyı bul
			//view modelden gelen nesneyi foreach le ve gerekli işlemleri yap

			//role atamasının gerçekleşmesi için gir çık yaptır

			//user index e yönlendir

			//Console.WriteLine("========================================================================");

			//Console.WriteLine("userEmail is " + userEmail);

			//AppUser foundUserbyEmail = await _userManager.FindByEmailAsync(userEmail);

			//if (foundUserbyEmail is null)
			//{
			//	return NotFound("Kullanıcı bulunamadı");

			//}


			//foreach (SetRoleForUserViewModel setRoleViewModel in setRoleViewModels)
			//{
			//	if (setRoleViewModel.State)
			//	{
			//		await _userManager.AddToRoleAsync(foundUserbyEmail, setRoleViewModel.RoleName);
			//	}
			//	else
			//	{
			//		await _userManager.RemoveFromRoleAsync(foundUserbyEmail, setRoleViewModel.RoleName);
			//	}
			//}
			////roller ayarlandı ancak etkili olması için giriş çıkış yapılması gerekli(burası biraz sıkıntılı,bakılması lazım)
			//AppUser user = await _userManager.GetUserAsync(User);
			//if (user == foundUserbyEmail)
			//{
			//	await _signInManager.SignOutAsync();
			//	await _signInManager.SignInAsync(foundUserbyEmail, true);
			//}
			//else
			//{
			//	await _signInManager.SignOutAsync();
			//}
			return RedirectToAction("Index", "User");
		}




	}
	//end
}
