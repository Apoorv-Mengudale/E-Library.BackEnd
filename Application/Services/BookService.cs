using Application.Authorization;
using AutoMapper;
using Domain;
using Domain.Models;
namespace Application.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        void Register(Book model);
        void Update(int id, UpdateBookRequest model);
        void Delete(int id);
        IEnumerable<Book> _GetBooksPaginated(int pageNumber, int pageSize, string? searchText);
    }

    public class BookService : IBookService
    {
        private ApplicationDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public BookService(
            ApplicationDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books;
        }

        public Book GetById(int id)
        {
            return getBook(id);
        }

        public void Register(Book model)
        {
            // map model to new Book object
            var Book = _mapper.Map<Book>(model);

            // save Book
            _context.Books.Add(Book);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateBookRequest model)
        {
            var Book = getBook(id);

            // copy model to Book and save
            _mapper.Map(model, Book);
            _context.Books.Update(Book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var Book = getBook(id);
            _context.Books.Remove(Book);
            _context.SaveChanges();
        }

        // helper methods

        private Book getBook(int id)
        {
            var Book = _context.Books.Find(id);
            if (Book == null) throw new KeyNotFoundException("Book not found");
            return Book;
        }

        public IEnumerable<Book> _GetBooksPaginated(int pageNumber, int pageSize, string? searchText)
        {
            if (pageNumber == 0) pageNumber = 1;
            if (searchText.Length != 0) searchText = searchText.ToLower();
            return _context.Books
            .Where(
                a => a.BookName.ToLower().Contains(searchText) ||
                 a.AuthorName.ToLower().Contains(searchText) ||
                  a.IntrestedArea.ToLower().Contains(searchText) ||
                   a.YearOfIssue.ToString().Contains(searchText))
                   .Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id);
        }

    }
}
