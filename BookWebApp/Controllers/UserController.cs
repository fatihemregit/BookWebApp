using AutoMapper;
using Business;
using Business.Abstracts.Auth;
using Business.Concretes.Auth;
using Entity.Auth;
using Entity.Exceptions.IAuthUserService;
using Entity.IAuthUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApp.Controllers
{
    public class UserController : Controller
    {

        //private readonly UserManager<AppUser> _userManager;

        //private readonly SignInManager<AppUser> _signInManager;
        //private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IAuthUserService _userService;
        private readonly ILogger<UserController> _logger;



        /* username:email:password
         *  admin:admin@admin.com:&dY3#cQjA8d!BAZo9vYLidDg
         *  
         */

        public UserController(/*UserManager<AppUser> userManager,*/ IMapper mapper, /*SignInManager<AppUser> signInManager,*/ /*RoleManager<AppRole> roleManager,*/ IAuthUserService userService, ILogger<UserController> logger)
        {
            //_userManager = userManager;
            _mapper = mapper;
            //_signInManager = signInManager;
            //_roleManager = roleManager;
            _userService = userService;
            _logger = logger;
        }
        [Authorize(Roles = "user_list")]
        public async Task<IActionResult> Index()
        {

            //yetkiye göre butonları gösterme(delete user,Manage Roles,Roles(column)) başlangıç
            Dictionary<string, bool> checkRoleswithLocalUserName = await _userService.checkRoleswithLocalUserName(
                                                                   User.Identity.Name,
                                                                   new List<string> { "user_delete", "roles_list_in_user", "role_set" });
            ViewData["user_delete"] = checkRoleswithLocalUserName["user_delete"];
            ViewData["roles_list_in_user"] = checkRoleswithLocalUserName["roles_list_in_user"];
            ViewData["role_set"] = checkRoleswithLocalUserName["role_set"];
            //yetkiye göre butonları gösterme(delete user,Manage Roles,Roles(column)) bitiş
            //oturum açan kullanıcının email ini viewData ya gönderme
            ViewData["currentUser"] = ((await _userService.findLocalUserwithUserName(User.Identity.Name)) is null) ? "": (await _userService.findLocalUserwithUserName(User.Identity.Name)).Email;

            //tüm kullanıcıları view e gönderme
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

                Exception result = await _userService.SignIn(_mapper.Map<IAuthUserServiceSignIn>(appUserViewModel));
                if (result is IAuthUserServiceSignInNotSucceeded)
                {
                    IAuthUserServiceSignInNotSucceeded customResult = (IAuthUserServiceSignInNotSucceeded)result;
                    if (customResult.Errors is null)
                    {
                        return BadRequest(customResult.Message);
                    }
                    customResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                }
                else
                {
                    return RedirectToAction("Index");
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
            if (ModelState.IsValid)
            {
                Console.WriteLine("model valid");
                if (TempData["returnUrl"] is null)
                {
                    TempData["returnUrl"] = "/";

                }

                Exception result = await _userService.Login(_mapper.Map<IAuthUserServiceLogin>(loginViewModel));
                if (result is IAuthUserServiceLoginNotSucceeded)
                {
                    if (result.Message == "user parameters is null")
                    {
                        return BadRequest(result.Message);
                    }
                    ModelState.AddModelError("NotUser", "Böyle bir kullanıcı bulunmamaktadır.");
                    ModelState.AddModelError("NotUser2", "E-posta veya şifre yanlış.");
                }
                else
                {
                    return Redirect(TempData["returnUrl"].ToString());
                }
            }
            return View(loginViewModel);

        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "user_delete")]
        public async Task<IActionResult> DeleteUser(string UserName)
        {

            Exception result = await _userService.DeleteUser(UserName);
            if (result is IAuthUserServiceDeleteUserNotSucceeded)
            {
                return BadRequest(result.Message);
            }
            else
            {
                Console.WriteLine("kullanıcı silindi");
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
            return await _userService.checkUserIsLogin(User.Identity.Name);
        }


    }
}
