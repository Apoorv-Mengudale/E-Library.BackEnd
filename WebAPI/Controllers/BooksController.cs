using Application.Authorization;
using Application.Services;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private IBookService _bookService;
        private IMapper _mapper;
        public BooksController(
            IBookService bookService,
            IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [Authorize(Role.Staff)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _bookService.GetAll();
            return Ok(users);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var user = _bookService.GetById(id);
            return Ok(user);
        }

        [HttpPost("addBook")]
        public IActionResult Register(Book model)
        {
            _bookService.Register(model);
            return Ok(new { message = "Book added successfully" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateBookRequest model)
        {
            _bookService.Update(id, model);
            return Ok(new { message = "Book updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);
            return Ok(new { message = "Book deleted successfully" });
        }

        [Authorize(Role.Staff)]
        [HttpGet("GetBooksPaginated")]
        public IActionResult GetBooksPaginated(int pageNumber, int pageSize, string? searchText)
        {
            if (searchText == null) searchText = "";
            var res = _bookService._GetBooksPaginated(pageNumber, pageSize, searchText);
            return Ok(new { data = res, totalCount = _bookService.GetAll().Count() });
        }
    }
}
