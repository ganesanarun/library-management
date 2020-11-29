using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog_Server.books
{
    public class Book
    {
        [Key] [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; } = null!;
        [Required] public string Author { get; set; } = null!;
    }
}