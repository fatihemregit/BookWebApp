using AutoMapper;
using BookWebApp.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApp.Controllers
{
	public class RoleController : Controller
	{

		private readonly RoleManager<AppRole> _roleManager;

		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;

		public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IMapper mapper)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_mapper = mapper;
		}

		public IActionResult Index()
		{

			return View();
		}

		[HttpGet]
		public IActionResult CreateRole()
		{
			return View();
		}

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

		[HttpGet]
		public async Task<IActionResult> SetRole(string userEmail = null)
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
			List<SetRoleViewModel> setRoleViewModels = new List<SetRoleViewModel>();
			//get All Roles in db
			IQueryable<AppRole> allRoles = _roleManager.Roles;


			foreach (AppRole role in allRoles)
			{
				SetRoleViewModel setRoleViewModel = new SetRoleViewModel();
				setRoleViewModel.RoleName = role.Name;
				setRoleViewModel.State = foundUserRoles.Contains(role.Name);
				setRoleViewModels.Add(setRoleViewModel);
			}
			TempData["userEmail"] = userEmail;

			return View(setRoleViewModels);
		}
		[HttpPost]
		public async Task<IActionResult> SetRole(List<SetRoleViewModel> setRoleViewModels,string userEmail)
		{
			Console.WriteLine("========================================================================");

            Console.WriteLine("userEmail is " + userEmail);

			AppUser foundUserbyEmail = await _userManager.FindByEmailAsync(userEmail);

			if (foundUserbyEmail is null)
			{
				return NotFound("Kullanıcı bulunamadı");
			
			}

			foreach (SetRoleViewModel setRoleViewModel in setRoleViewModels)
			{
				if (setRoleViewModel.State)
				{
					await _userManager.AddToRoleAsync(foundUserbyEmail,setRoleViewModel.RoleName);
				}
				else
				{
					await _userManager.RemoveFromRoleAsync(foundUserbyEmail,setRoleViewModel.RoleName);
				}
			}
			return RedirectToAction("Index", "User");
		}


	}
}
