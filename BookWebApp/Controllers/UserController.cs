using AutoMapper;
using BookWebApp.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApp.Controllers
{
    public class UserController : Controller
    {

        /*
         * username:email:password
         * fterm:fatih@admin.com:6YyRLyKJEt@k  (kayıtlı)
         * fatihKILINC:fatih@test.com:Qwe12. (kayıtlı)
         */

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IMapper _mapper;


        public UserController(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }



        [Authorize]
        public IActionResult Index()
        {

            return View(_mapper.Map<IEnumerable<AppUserViewModel>>(_userManager.Users));
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



    }
}
