using AutoMapper;
using Domain;
using Domain.Models;
using Domain.Models.Books;

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
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books;
        }

        public Book GetById(int id)
        {
            return GetBook(id);
        }

        public void Register(Book model)
        {
            // map model to new Book object
            var book = _mapper.Map<Book>(model);

            // save Book
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateBookRequest model)
        {
            var book = GetBook(id);

            // copy model to Book and save
            _mapper.Map(model, book);
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = GetBook(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        // helper methods

        private Book GetBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) throw new KeyNotFoundException("Book not found");
            return book;
        }

        public IEnumerable<Book> _GetBooksPaginated(int pageNumber, int pageSize, string? searchText)
        {
            if (pageNumber == 0) pageNumber = 1;
            if (!string.IsNullOrEmpty(searchText)) searchText = searchText.ToLower();
            return _context.Books
            .Where(
                a => searchText != null && (a.BookName.ToLower().Contains(searchText) ||
                                            a.AuthorName.ToLower().Contains(searchText) ||
                                            a.IntrestedArea.ToLower().Contains(searchText) ||
                                            a.YearOfIssue.ToString().Contains(searchText)))
                   .Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id);
        }

    }
}
