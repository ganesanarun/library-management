using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Search_Server.Books
{
    public class BooksRepresentation
    {
        [Required] public IReadOnlyCollection<Book> Books { get; set; } = null!;

        [Required] public PagingRepresentation Paging { get; set; } = null!;
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class PagingRepresentation
    {
        public long Total { get; set; }

        public short From { get; set; }

        public short Size { get; set; }
    }
}