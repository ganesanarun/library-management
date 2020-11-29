using System.ComponentModel.DataAnnotations;

namespace Catalog_Server.books
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class ValueBook
    {
        [Required] public string Name { get; set; } = null!;
        [Required] public string Author { get; set; } = null!;

        public Book ToBook()
        {
            return new Book
            {
                Name = Name,
                Author = Author
            };
        }
    }
}