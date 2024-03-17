using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
}