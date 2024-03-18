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
            var result = await (from b in _context.Books
                                join au in _context.Authors
                                on b.AuthorId equals au.Id
                                select new
                                {
                                    Id = b.Id,
                                    Title = b.Title,
                                    Price = b.Price,
                                    Author = string.Format("{0} {1}", au.FirstName, au.LastName)
                                }
            ).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var result = await (from b in _context.Books
                                join au in _context.Authors
                                on b.AuthorId equals au.Id
                                select new
                                {
                                    Id = b.Id,
                                    Title = b.Title,
                                    Price = b.Price,
                                    Author = string.Format("{0} {1}", au.FirstName, au.LastName)
                                }
            ).FirstOrDefaultAsync();
            return result != null ? Ok(result) : NotFound("Did not find");
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDto bookRequest)
        {
            _context.Books.Add(new Book()
            {
                Id = Guid.NewGuid(),
                Title = bookRequest.Title,
                Price = bookRequest.Price,
                AuthorId = bookRequest.AuthorId,
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
                book.AuthorId = bookRequest.AuthorId;
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