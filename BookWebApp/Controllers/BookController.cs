using AutoMapper;
using BookWebApp.Data.Context;
using BookWebApp.Models.Auth;
using BookWebApp.Models.Dto;
using BookWebApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BookWebApp.Controllers
{

    
    public class BookController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;
        private readonly UserManager<AppUser> _userManager;


        public BookController(ApplicationDbContext context, IMapper mapper, ILogger<BookController> logger, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            //kullanıcının yetkilerine göre create,edit ve delete butonlarını gösterip göstermeme(details i herkes görebilir) başlangıç
            //oturum açan kullanıcıyı bulma
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
            //bookdtos mapping
            IEnumerable<BookDto> bookDtos = _context.BookDtos.ToList();
            IEnumerable<BookViewModelForList> bookViewModels = _mapper.Map<IEnumerable<BookViewModelForList>>(bookDtos);
            return View(bookViewModels);
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
        public IActionResult Create([FromForm]BookViewModelForCreate bookViewModelForCreate)
        {
            if (bookViewModelForCreate is null)
            {
                return BadRequest();
            }
            //aynı kitabı ekleme yi engelleme(proje bittikten sonra bak)
            _context.BookDtos.Add(_mapper.Map<BookDto>(bookViewModelForCreate));
            _context.SaveChanges();
            return RedirectToAction("Index", "Book");
        }


        //edit
        [Authorize(Roles = "book_edit")]
        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit([FromRoute]int id)
        {
            _logger.LogInformation("edit get çalıştı");

            //id is null
            if (id == 0)
            {
                return BadRequest();
            }
            //id is not null
            //find a object with id start
            BookDto foundBookDto = _context.BookDtos.Where(bd => bd.Id == id).SingleOrDefault();
            //foundBookDto is null
            if (foundBookDto is null)
            {
                return NotFound();

            }

            //foundBookDto is not null
            //find a object with id end
            //map bookDto to bookViewModel
            //idsiz işlem yapmayı deneyelim daha sonra
            _logger.LogWarning($"(edit get function) book dto price is {foundBookDto.Price}");
            BookViewModelForUpdate foundBookViewModelForUpdate = _mapper.Map<BookViewModelForUpdate>(foundBookDto);

            return View(foundBookViewModelForUpdate);
        }

        [Authorize(Roles = "book_edit")]
        [HttpPost("Edit/{id:int}")]
        public IActionResult Edit([FromRoute]int id, [FromForm]BookViewModelForUpdate bookViewModelForUpdate)
        {
            _logger.LogInformation("edit post çalıştı");
            if (bookViewModelForUpdate is null)
            {
                _logger.LogInformation("bookViewModelForUpdate is null if ine girildi");
                return BadRequest();
            }
            _logger.LogInformation("bookViewModelForUpdate is null if ine girilmedi");
            _logger.LogWarning($"BookViewModelForUpdate price is {bookViewModelForUpdate.Price}");
            //update işlemi
            _context.BookDtos.Update(_mapper.Map<BookDto>(bookViewModelForUpdate));
            _context.SaveChanges();
            return RedirectToAction("Index", "Book");
        }

        //Details

        [HttpGet("Details/{id:int}")]
        public IActionResult Details([FromRoute]int id)
        {

            //id is null
            if (id == 0)
            {
                return BadRequest();
            }
            //is is not null
            
            //find a bookdto

            BookDto foundBookDto = _context.BookDtos.Where(bd => bd.Id == id).SingleOrDefault();
            //foundBookDto is null
            if (foundBookDto is null)
            {
                return NotFound();
            }

            //foundBookDto is not null

            //convert BookDto to BookViewModelForDetails
            BookViewModelForDetails foundbookViewModelForDetails = _mapper.Map<BookViewModelForDetails>(foundBookDto);

            return View(foundbookViewModelForDetails);
        }

        [Authorize(Roles = "book_delete")]
        [HttpGet("Delete/{id:int}")]
        public IActionResult Delete([FromRoute]int id)
        {
            //id is null
            if (id == 0)
            {
                return BadRequest();
            }
            //id is not null


            //find a bookDto
            BookDto foundBookDto = _context.BookDtos.Where(bd => bd.Id == id).SingleOrDefault();
            //foundBookDto is null
            if (foundBookDto is null)
            {
                return NotFound();
            }
            //foundBookDto is not null
            //convert bookDto to BookViewModelForDelete
            BookViewModelForDelete foundBookViewModelForDelete = _mapper.Map<BookViewModelForDelete>(foundBookDto);
            return View(foundBookViewModelForDelete);
        }


        [Authorize(Roles = "book_delete")]
        [HttpPost("Delete/{id:int}")]
        public IActionResult DeletePost([FromRoute] int id)
        {
            //id is null

            if (id == 0)
            {
                return BadRequest();
            }
            //is is not null

            //find a bookdto

            BookDto foundBookDto = _context.BookDtos.Where(bd => bd.Id == id).SingleOrDefault();
            //foundBookDto is null

            if (foundBookDto is null) { 
                return NotFound();
            
            }
            //foundBookDto is null
            //safe delete bookDto(changing the isdeleted value)
            foundBookDto.isDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("Index","Book");
        }
    }
}
