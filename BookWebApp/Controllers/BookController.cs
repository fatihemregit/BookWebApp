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
using Business.Concretes.Auth;
using Business.Abstracts.Auth;
using Entity.IAuthUserService;

namespace BookWebApp.Controllers
{


    public class BookController : Controller
    {

        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;
        //private readonly UserManager<AppUser> _userManager;
        private readonly IAuthUserService _userService;
        private readonly IBookService _bookService;


        public BookController(IMapper mapper, ILogger<BookController> logger, /*UserManager<AppUser> userManager,*/ IBookService bookService, IAuthUserService userService)
        {
            _mapper = mapper;
            _logger = logger;
            //_userManager = userManager;
            _bookService = bookService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            //kullanıcının yetkilerine göre create,edit ve delete butonlarını gösterip göstermeme(details i herkes görebilir) başlangıç

            Dictionary<string, bool> checkRoleswithLocalUserName = await _userService.checkRoleswithLocalUserName(
                                                                    User.Identity.Name, 
                                                                    new List<string> { "book_create", "book_edit", "book_delete" });
            
            ViewData["book_create"] = checkRoleswithLocalUserName["book_create"];
            ViewData["book_edit"] = checkRoleswithLocalUserName["book_edit"];
            ViewData["book_delete"] = checkRoleswithLocalUserName["book_delete"];
            //kullanıcının yetkilerine göre create,edit ve delete butonlarını gösterip göstermeme(details i herkes görebilir) bitiş
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