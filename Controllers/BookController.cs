using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BookStore.Dto;
using BookStore.Models;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly DataContext _context;

        public BookController(ILogger<BookController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBook()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            return book != null ? Ok(book) : NotFound("Did not find");
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDto bookRequest)
        {

            _context.Books.Add(new Book()
            {
                Id = Guid.NewGuid(),
                Title = bookRequest.Title,
                Price = bookRequest.Price,
            });
            await _context.SaveChangesAsync();

            return Ok(bookRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BookDto bookRequest)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                book.Title = bookRequest.Title;
                book.Price = bookRequest.Price;
                await _context.SaveChangesAsync();
                return Ok(book);
            }
            return NotFound("Didn't find book");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return Ok(book);
            }
            return NotFound("Didn't find book");

        }
    }
}