using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }

        protected void OModelCreating(ModelBuilder builder)
        {
            builder.Entity<AuthorBook>()
            .HasKey(k => new { k.AuthorId, k.BookId });
        }
    }
}