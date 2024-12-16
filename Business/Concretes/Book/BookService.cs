using AutoMapper;
using Business.Abstracts.Book;
using Data.Abstracts.Book;
using Entity.Exceptions.IBookService;
using Entity.IBookRepository;
using Entity.IBookService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes.Book
{
    public class BookService : IBookService
    {


        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Exception> getAll()
        {
            List<IBookServiceGetAllBook> getAllBooks = _mapper.Map<List<IBookServiceGetAllBook>>(await _bookRepository.getAll());
            return new IBookServiceGetAllBookSucceeded("getAll is succeeded", getAllBooks);
        }


        public async Task<Exception> createOneBook(IBookServiceCreateOneBook Book)
        {
            if (Book is null)
            {
                //daha sonrasında(hata yönetimi eklendiğinde) hata fırlat ama şimdilik null döndürelim
                return new IBookServiceCreateOneBookNotSucceeded("Book Parameter is null");
            }
            IBookRepositoryCreateOneBook createOneBook = await _bookRepository.createOneBook(_mapper.Map<IBookRepositoryCreateOneBook>(Book));
            return new IBookServiceCreateOneBookSucceeded("createOneBook is succeeded", _mapper.Map<IBookServiceCreateOneBook>(createOneBook));

        }

        public async Task<Exception> editOneBookById(int id, IBookServiceEditOneBookById Book)
        {
            if ((id == 0) || (Book is null))
            {
                //daha sonrasında(hata yönetimi eklendiğinde) hata fırlat ama şimdilik null döndürelim
                return new IBookServiceEditOneBookByIdNotSucceeded("some parameters are null");
            }
            IBookRepositoryEditOneBookById? editOneBookById = await _bookRepository.editOneBookById(id, _mapper.Map<IBookRepositoryEditOneBookById>(Book));
            if (editOneBookById is null)
            {
                //return null;
                return new IBookServiceEditOneBookByIdNotSucceeded("editOneBookById is null");
            }
            return new IBookServiceEditOneBookByIdSucceeded("editOneBookById is succeeded", _mapper.Map<IBookServiceEditOneBookById>(editOneBookById));
        }

        public async Task<Exception> getOneBookById(int id)
        {
            if (id == 0)
            {
                //daha sonrasında(hata yönetimi eklendiğinde) hata fırlat ama şimdilik null döndürelim
                return new IBookServiceGetOneBookByIdNotSucceeded("id parameter is null");
            }
            IBookRepositoryGetOneBookById? getOneBookByIdFromRepository = await _bookRepository.getOneBookById(id);
            if (getOneBookByIdFromRepository == null)
            {
                //daha sonrasında(hata yönetimi eklendiğinde) hata fırlat ama şimdilik null döndürelim
                return new IBookServiceGetOneBookByIdNotSucceeded("getOneBookByIdFromRepository is null");
            }

            return new IBookServiceGetOneBookByIdSucceeded("getOneBookById is succeeded", _mapper.Map<IBookServiceGetOneBookById>(getOneBookByIdFromRepository));


        }

        public async Task<Exception> deleteOneBookById(int id)
        {
            await _bookRepository.deleteOneBookById(id);
            return new IBookServiceDeleteOneBookByIdSucceeded("deleteOneBookById is succeeded");
        }





    }
}
