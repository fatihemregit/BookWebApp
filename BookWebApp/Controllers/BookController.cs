using AutoMapper;
using Entity.Auth;
using Entity.IBookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Entity.ViewModel;
using Business.Abstracts.Book;

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
			List<BookViewModelForList> bookViewModelForLists = _mapper.Map<List<BookViewModelForList>>(await _bookService.getAll());
            return View(bookViewModelForLists);
        }
        //create

        [Authorize(Roles = "book_create")]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "book_create")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] BookViewModelForCreate bookViewModelForCreate)
        {
            //aynı kitabı ekleme yi engelleme(proje bittikten sonra bak)(bu kural business a yazılabilir)
            IBookServiceCreateOneBook? createOneBook = await _bookService.createOneBook(_mapper.Map<IBookServiceCreateOneBook>(bookViewModelForCreate));
            if (createOneBook is null)
            {
                return RedirectToAction("MyErrorPage", "Book", new { errorMessage = "createOneBook is null(this error comes from Bookcontroller/Create(post))" });
            }

            return RedirectToAction("Index", "Book");
        }


        //edit
        [Authorize(Roles = "book_edit")]
        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            IBookServiceGetOneBookById? getOneBookById = await _bookService.getOneBookById(id);
            if (getOneBookById is null)
            {
				return RedirectToAction("MyErrorPage", "Book", new { errorMessage = "getOneBookById is null(this error comes from Bookcontroller/Edit)" });
			}
            return View(_mapper.Map<BookViewModelForUpdate>(getOneBookById));

		}

        [Authorize(Roles = "book_edit")]
        [HttpPost("Edit/{id:int}")]
        public async Task<IActionResult >Edit([FromRoute] int id, [FromForm] BookViewModelForUpdate bookViewModelForUpdate)
        {
            IBookServiceEditOneBookById? editOneBookById = await _bookService.editOneBookById(id,_mapper.Map<IBookServiceEditOneBookById>(bookViewModelForUpdate));
            if (editOneBookById is null)
            {
				return RedirectToAction("MyErrorPage", "Book", new { errorMessage = "editOneBookById is null(this error comes from Bookcontroller/Edit(post))" });
			}
			return RedirectToAction("Index", "Book");

		}

        //Details

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult >Details([FromRoute] int id)
        {
            IBookServiceGetOneBookById? getOneBookById = await _bookService.getOneBookById(id);
            if (getOneBookById is null)
            {
				return RedirectToAction("MyErrorPage", "Book", new { errorMessage = "getOneBookById is null(this error comes from Bookcontroller/Details)" });
			}

			return View(_mapper.Map<BookViewModelForDetails>(getOneBookById));
        }

        [Authorize(Roles = "book_delete")]
        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            IBookServiceGetOneBookById? getOneBookById = await _bookService.getOneBookById(id);
            if (getOneBookById is null)
            {
				return RedirectToAction("MyErrorPage", "Book", new { errorMessage = "getOneBookById is null(this error comes from Bookcontroller/Delete)" });
			}
            return View(_mapper.Map<BookViewModelForDelete>(getOneBookById));
		}


		[Authorize(Roles = "book_delete")]
        [HttpPost("Delete/{id:int}")]
        public IActionResult DeletePost([FromRoute] int id)
        {
            _bookService.deleteOneBookById(id);
            return RedirectToAction("Index", "Book");
        }

        public IActionResult MyErrorPage(string errorMessage)
        {
            //view i yok(oluştur)
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

    }
}
