using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BookStore.Models;
using BookStore.Dto;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly DataContext _context;

        public AuthorController(ILogger<AuthorController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthor()
        {
            return Ok(await _context.Authors.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(Guid id)
        {
            var authors = await _context.Authors.FindAsync(id);
            return authors != null ? Ok(authors) : NotFound("Author not found");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorDto requestAuthor)
        {
            _context.Authors.Add(new Author()
            {
                Id = Guid.NewGuid(),
                FirstName = requestAuthor.FirstName,
                LastName = requestAuthor.LastName,
                Email = requestAuthor.Email,
                Phone = requestAuthor.Phone,
            });
            await _context.SaveChangesAsync();
            return Ok(requestAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, AuthorDto requestAuthor)
        {
            var authors = await _context.Authors.FindAsync(id);
            if (authors != null)
            {
                authors.FirstName = requestAuthor.FirstName;
                authors.LastName = requestAuthor.LastName;
                authors.Email = requestAuthor.Email;
                authors.Phone = requestAuthor.Phone;

                await _context.SaveChangesAsync();
                return Ok(authors);
            }
            return NotFound("Author not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var authors = await _context.Authors.FindAsync(id);
            if (authors == null)
                return NotFound("Author not found");

            var count = await _context.Books.CountAsync(x => x.AuthorId == id);
            if (count > 0)
                return BadRequest("Can't delete author when there are book write by this author");

            _context.Authors.Remove(authors);
            await _context.SaveChangesAsync();
            return Ok("Removed author successfully");
        }

    }
}