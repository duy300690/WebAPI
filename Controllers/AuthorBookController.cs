using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    public class AuthorBookController : Controller
    {
        private readonly ILogger<AuthorBookController> _logger;
        private readonly DataContext _context;

        public AuthorBookController(ILogger<AuthorBookController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.AuthorBooks.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _context.AuthorBooks.FindAsync(id);

            return result != null ? Ok(result) : NotFound("Author not found");
        }



    }
}