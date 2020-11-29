using System.ComponentModel.DataAnnotations;

namespace Catalog_Server.books
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public struct ValueBook
    {
        [Required] public string Name { get; set; }
        [Required] public string Author { get; set; }

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