using System;

namespace Search_Server.Books
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Book
    {
        public string Name { get; set; } = null!;
        public string Author { get; set; } = null!;
        public Guid Id { get; set; }

        public override string ToString()
        {
            return $"{Name} written by {Author} with an Id {Id}";
        }
    }
}