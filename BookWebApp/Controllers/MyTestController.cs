using AutoMapper;
using Entity.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Data.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace BookWebApp.Controllers
{
    public class MyTestController : Controller
    {

        //test etmek istediğimiz olay : kulanıcıya rol eklerken kullanıcıda hangi fieldler olmalı

        //test rolü
        // Adı : MytestRole 
        //Id : cc2e2d0c-1827-471b-c2cd-08dd1ad6b53c


        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;


        public MyTestController(IMapper mapper, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            // Console.WriteLine("Index fonksiyonu çalıştı");


            //IdentityResult identityResult = await _userManager.AddToRoleAsync(, "MytestRole");


            // if (identityResult.Succeeded)
            // {
            //     //işlem başarılı
            //     Console.WriteLine("işlem başarılı oldu");
            //     return RedirectToAction("Index", "User");
            // }
            // foreach (var item in identityResult.Errors)
            // {
            //     Console.WriteLine($" hata açıklaması:  {item.Description}");
            // }

            return RedirectToAction("Index", "Role");
        }

        //public async Task<IActionResult> deleterolefromUser()
        //{
        //	return View();
        //}



    }

}
