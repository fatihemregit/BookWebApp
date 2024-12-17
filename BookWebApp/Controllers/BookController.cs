using AutoMapper;
using Entity.Auth;
using Entity.IBookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Entity.ViewModel;
using Business.Abstracts.Book;
using Entity.Exceptions.IBookService;

namespace BookWebApp.Controllers
{


    public class BookController : Controller
    {

        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;
        private readonly UserManager<AppUser> _userManager;

        private readonly IBookService _bookService;


        public BookController(IMapper mapper, ILogger<BookController> logger, UserManager<AppUser> userManager, IBookService bookService)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            //kullanıcının yetkilerine göre create,edit ve delete butonlarını gösterip göstermeme(details i herkes görebilir) başlangıç
            //oturum açan kullanıcıyı bulma
            _logger.LogCritical("Index tetiklendi");
            var user = await _userManager.GetUserAsync(User);
            //eğer oturum açan kullanıcı yoksa "user" değişkeni null gelir
            //oturum açan kullanıcı yoksa yapılacaklar
            if (user is null)
            {
                //oturum açan kullanıcı yoksa  create,edit ve delete butonları gözükmez
                ViewData["book_create"] = false;
                ViewData["book_edit"] = false;
                ViewData["book_delete"] = false;
            }
            //oturum açan kullanıcı var
            //kullanıcının yetkilerini öğrenme ve yetkileri view datalara atama
            else
            {
                ViewData["book_create"] = await _userManager.IsInRoleAsync(user, "book_create");
                ViewData["book_edit"] = await _userManager.IsInRoleAsync(user, "book_edit");
                ViewData["book_delete"] = await _userManager.IsInRoleAsync(user, "book_delete");
            }
            //kullanıcının yetkilerine göre create,edit ve delete butonlarını gösterip göstermeme(details i herkes görebilir) bitiş
            //IBookServiceGetAllBook mapping
            //List<BookViewModelForList> bookViewModelForLists = _mapper.Map<List<BookViewModelForList>>(await _bookService.getAll());
            Exception result = await _bookService.getAll();
            if (result is IBookServiceGetAllBookSucceeded)
            {

                List<BookViewModelForList> bookViewModelForLists = _mapper.Map<List<BookViewModelForList>>(((IBookServiceGetAllBookSucceeded)result).AllBooks);
                return View(bookViewModelForLists);
            }
            return RedirectToAction("MyErrorPage", "Book", new { errorMessage = $"{result.Message}(this error comes from Bookcontroller/Index" });
        }
        //create

        [Authorize(Roles = "book_create")]
        [HttpGet("BookCreate")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "book_create")]
        [HttpPost("BookCreate")]
        public async Task<IActionResult> Create([FromForm] BookViewModelForCreate bookViewModelForCreate)
        {
            //aynı kitabı ekleme yi engelleme(proje bittikten sonra bak)(bu kural business a yazılabilir)
            Exception result = await _bookService.createOneBook(_mapper.Map<IBookServiceCreateOneBook>(bookViewModelForCreate));
            if (result is IBookServiceCreateOneBookSucceeded)
            {
                return RedirectToAction("Index", "Book");
            }
            return RedirectToAction("MyErrorPage", "Book", new { errorMessage = $"{result.Message}(this error comes from Bookcontroller/Create(post))" });
        }


        //edit
        [Authorize(Roles = "book_edit")]
        [HttpGet("BookEdit/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Exception result = await _bookService.getOneBookById(id);
            if (result is IBookServiceGetOneBookByIdSucceeded)
            {
                return View(_mapper.Map<BookViewModelForUpdate>(((IBookServiceGetOneBookByIdSucceeded)result).Book));
            }
            return RedirectToAction("MyErrorPage", "Book", new { errorMessage = $"{result.Message}(this error comes from Bookcontroller/Edit)" });


        }

        [Authorize(Roles = "book_edit")]
        [HttpPost("BookEdit/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] BookViewModelForUpdate bookViewModelForUpdate)
        {
            Exception result = await _bookService.editOneBookById(id, _mapper.Map<IBookServiceEditOneBookById>(bookViewModelForUpdate));
            if (result is IBookServiceEditOneBookByIdSucceeded)
            {
                return RedirectToAction("Index", "Book");
            }
            return RedirectToAction("MyErrorPage", "Book", new { errorMessage = $"{result.Message}(this error comes from Bookcontroller/Edit(post))" });

        }

        //Details

        [HttpGet("BookDetails/{id:int}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            Exception result = await _bookService.getOneBookById(id);
            if (result is IBookServiceGetOneBookByIdSucceeded)
            {
                return View(_mapper.Map<BookViewModelForDetails>(((IBookServiceGetOneBookByIdSucceeded)result).Book));
            }
            return RedirectToAction("MyErrorPage", "Book", new { errorMessage = $"{result.Message}(this error comes from Bookcontroller/Details)" });

        }

        [Authorize(Roles = "book_delete")]
        [HttpGet("BookDelete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Exception result = await _bookService.getOneBookById(id);
            if (result is IBookServiceGetOneBookByIdSucceeded)
            {
                return View(_mapper.Map<BookViewModelForDelete>(((IBookServiceGetOneBookByIdSucceeded)result).Book));
            }
            return RedirectToAction("MyErrorPage", "Book", new { errorMessage = $"{result.Message}(this error comes from Bookcontroller/Delete)" });

        }


        [Authorize(Roles = "book_delete")]
        [HttpPost("BookDelete/{id:int}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            Exception result = await _bookService.deleteOneBookById(id);
            if (result is IBookServiceDeleteOneBookByIdSucceeded)
            {
                return RedirectToAction("Index", "Book");
            }
            return RedirectToAction("MyErrorPage", "Book", new { errorMessage = $"{result.Message}(this error comes from Bookcontroller/Delete(Post))" });
        }

        public IActionResult MyErrorPage(string errorMessage)
        {
            //view i yok(oluştur)
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

    }
}