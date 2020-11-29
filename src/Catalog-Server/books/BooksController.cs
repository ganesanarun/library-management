using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Server.books
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BooksController
    {
        private readonly BookContext bookContext;

        public BooksController(BookContext bookContext)
        {
            this.bookContext = bookContext;
        }

        [Authorize(Policy = "GetBook")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Book>> GetABookWith(Guid id)
        {
            var mayBeBook = await bookContext.Books.FindAsync(id);
            return mayBeBook == null ? (ActionResult) new NotFoundResult() : new OkObjectResult(mayBeBook);
        }

        [Authorize(Policy = "AddBook")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateA(ValueBook book)
        {
            var aBook = await bookContext.Books.AddAsync(book.ToBook());
            await bookContext.SaveChangesAsync();

            return new CreatedAtActionResult(nameof(GetABookWith),
                "Books",
                new {id = aBook.Entity.Id},
                aBook.Entity);
        }

        [Authorize(Policy = "UpdateBook")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateABookWith(Guid id, [FromBody] ValueBook book)
        {
            var mayBeBook = await bookContext.Books.FindAsync(id);
            if (mayBeBook == null) return new NotFoundResult();

            mayBeBook.Author = book.Author;
            mayBeBook.Name = book.Name;

            bookContext.Entry(mayBeBook).State = EntityState.Modified;
            await bookContext.SaveChangesAsync();
            return new NoContentResult();
        }

        [Authorize(Policy = "DeleteBook")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteABookWith(Guid id)
        {
            var mayBeBook = await bookContext.Books.FindAsync(id);
            if (mayBeBook == null) return new NotFoundResult();

            bookContext.Books.Remove(mayBeBook);
            await bookContext.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}