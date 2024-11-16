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
		public IActionResult RemoveRoleFromUser()
		{

			return View();
		}

		public IActionResult RemoveRole()
		{
			return View();
		}


	}
}
