using AutoMapper;
using Business.Abstracts.Auth;
using Business.Concretes.Auth;
using Entity.Auth;
using Entity.IAuthUserService;
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
		private readonly IAuthUserService _userService;


		/* username:email:password
         *  admin:admin@admin.com:&dY3#cQjA8d!BAZo9vYLidDg
         *  
         */

		public UserController(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, AuthUserService userService)
		{
			_userManager = userManager;
			_mapper = mapper;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_userService = userService;
		}
		[Authorize(Roles = "user_list")]
		public async Task<IActionResult> Index()
		{
			//TAMAM
			//yetkiye göre butonları gösterme(delete user,Manage Roles,Roles(column)) başlangıç
			//oturum açan kullanıcıyı bulma
			var user = await _userManager.GetUserAsync(User);
			//eğer oturum açan kullanıcı yoksa "user" değişkeni null gelir
			//oturum açan kullanıcı yoksa yapılacaklar
			if (user is null)
			{
				ViewData["currentUser"] = "";
				ViewData["user_delete"] = false;
				ViewData["roles_list_in_user"] = false;
				ViewData["role_set"] = false;
			}
			else
			{
				ViewData["currentUser"] = user.Email.ToString();
				ViewData["user_delete"] = await _userManager.IsInRoleAsync(user, "user_delete");
				ViewData["roles_list_in_user"] = await _userManager.IsInRoleAsync(user, "roles_list_in_user");
				ViewData["role_set"] = await _userManager.IsInRoleAsync(user, "role_set");
			}

			//yetkiye göre butonları gösterme(delete user,Manage Roles,Roles(column)) bitiş

			return View(_mapper.Map<List<AppUserViewModel>>(await _userService.GetAllUsersAsync()));
		}


		[HttpGet]
		public async Task<IActionResult> SignIn()
		{
			bool userIsSignedIn = await checkUserIsLogin();
			if (userIsSignedIn)
			{
				return RedirectToAction("AccessDenied", "User", new { ErrorMessage = "Giriş yapan kullanıcı tekrar kayıt oluşturamaz" });
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(AppUserViewModel appUserViewModel)
		{
			//TAMAM
			if (ModelState.IsValid)
			{

				IdentityResult result = await _userService.CreateAsync(_mapper.Map<IAuthUserServiceCreateAsync>(appUserViewModel), appUserViewModel.Sifre);
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
		public async Task<IActionResult> Login(string returnUrl)
		{
			bool userIsSignedIn = await checkUserIsLogin();
			if (userIsSignedIn)
			{
				return RedirectToAction("AccessDenied", "User", new { ErrorMessage = "Giriş Yapan Kullanıcı tekrar giriş yapamaz(Sisteme Zaten Giriş Yaptınız)" });
			}
			TempData["returnUrl"] = returnUrl;
			return View();

		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			//TAMAM
			if (ModelState.IsValid)
			{
				AppUser founduser = _mapper.Map<AppUser>(await _userService.FindByEmailAsync(loginViewModel.Email));

				if (TempData["returnUrl"] is null)
				{
					TempData["returnUrl"] = "/";

				}

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

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index");
		}


		[Authorize(Roles = "user_delete")]
		public async Task<IActionResult> DeleteUser(string UserName)
		{
			Console.WriteLine("deleting users");
			AppUser founduser = _mapper.Map<AppUser>(await _userService.FindByNameAsync(UserName));
			if (founduser is null)
			{
				return NotFound();
			}
			Console.WriteLine("kullanıcı bulundu");
			Console.WriteLine("kullanıcı siliniyor");
			IdentityResult identityResult = await _userService.DeleteAsync(_mapper.Map<IAuthUserServiceDeleteAsync>(founduser));
			if (identityResult.Succeeded)
			{
				Console.WriteLine("kullanıcı silindi");
			}
			else
			{
                Console.WriteLine("kullanıcı silinmedi");
			}
			return RedirectToAction("Index", "User");
		}

		public IActionResult AccessDenied(string ErrorMessage = "Yetkilendirme Hatasi")
		{
			ViewBag.ErrorMessage = ErrorMessage;
			return View();
		}
		[NonAction]
		public async Task<bool> checkUserIsLogin()
		{
			bool result = false;
			var user = await _userManager.GetUserAsync(User);
			if (user is not null)
			{
				//client ta giriş yapmış kullanıcı var
				result = true;

			}
			return result;
		}


	}
}
