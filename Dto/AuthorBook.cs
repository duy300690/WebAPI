using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dto
{
    public class AuthorBook
    {
        public Guid AuthorId { get; set; }
        public Guid BookId { get; set; }
    }
}