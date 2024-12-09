using AutoMapper;
using Business.Abstracts.Book;
using Data.Abstracts.Book;
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

        public async Task<List<IBookServiceGetAllBook>> getAll()
        {
            return _mapper.Map<List<IBookServiceGetAllBook>>(await _bookRepository.getAll());
        }


        public async Task<IBookServiceCreateOneBook?> createOneBook(IBookServiceCreateOneBook Book)
        {
            if (Book is null)
            {
                //daha sonrasında(hata yönetimi eklendiğinde) hata fırlat ama şimdilik null döndürelim
                return null;
            }
            IBookRepositoryCreateOneBook? createOneBook = await _bookRepository.createOneBook(_mapper.Map<IBookRepositoryCreateOneBook>(Book));
            if (createOneBook is null)
            {
                return null;
            }
            return _mapper.Map<IBookServiceCreateOneBook>(createOneBook);


        }

        public async Task<IBookServiceEditOneBookById?> editOneBookById(int id, IBookServiceEditOneBookById Book)
        {
            if (id == 0)
            {
                //daha sonrasında(hata yönetimi eklendiğinde) hata fırlat ama şimdilik null döndürelim
                return null;
            }
            if (Book is null)
            {
                //daha sonrasında(hata yönetimi eklendiğinde) hata fırlat ama şimdilik null döndürelim

                return null;
            }
            IBookRepositoryEditOneBookById? editOneBookById = await _bookRepository.editOneBookById(id, _mapper.Map<IBookRepositoryEditOneBookById>(Book));
            if (editOneBookById is null)
            {
                return null;
            }
            return _mapper.Map<IBookServiceEditOneBookById>(editOneBookById);
        }

        public async Task<IBookServiceGetOneBookById?> getOneBookById(int id)
        {
            if (id == 0)
            {
                //daha sonrasında(hata yönetimi eklendiğinde) hata fırlat ama şimdilik null döndürelim
                return null;
            }
            IBookRepositoryGetOneBookById? getOneBookByIdFromRepository = await _bookRepository.getOneBookById(id);
            if (getOneBookByIdFromRepository == null)
            {
                //daha sonrasında(hata yönetimi eklendiğinde) hata fırlat ama şimdilik null döndürelim
                return null;

            }

            return _mapper.Map<IBookServiceGetOneBookById>(getOneBookByIdFromRepository);



        }

        public async Task deleteOneBookById(int id)
        {
            await _bookRepository.deleteOneBookById(id);
        }





    }
}
