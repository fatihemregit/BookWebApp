﻿using AutoMapper;
using BookWebApp.Data.Context;
using BookWebApp.Models.Dto;
using BookWebApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BookWebApp.Controllers
{
    public class BookController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;

        public BookController(ApplicationDbContext context, IMapper mapper, ILogger<BookController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //mapping
            IEnumerable<BookDto> bookDtos = _context.BookDtos.ToList();
            IEnumerable<BookViewModel> bookViewModels = _mapper.Map<IEnumerable<BookViewModel>>(bookDtos);
            return View(bookViewModels);
        }
        //create

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

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




    }
}
