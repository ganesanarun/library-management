using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Search_Server.Books
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BooksController
    {
        private readonly ElasticClient elasticClient;
        private const short MaxPageSize = 200;

        public BooksController(ElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        [Authorize(Policy = Search_Server.Constants.ListBooksPolicy)]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BooksRepresentation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooks([FromQuery(Name = "from")] short from,
            [FromQuery(Name = "size")] short size = MaxPageSize)
        {
            var pageSize = Math.Min(size, MaxPageSize);
            var searchResponse = await elasticClient.SearchAsync<Book>(s => s.Index(Constants.IndexName)
                .From(from)
                .Take(pageSize));

            var bookListRepresentation = new BooksRepresentation
            {
                Books = searchResponse.Documents,
                Paging = new PagingRepresentation
                {
                    From = from,
                    Size = (short) searchResponse.Documents.Count,
                    Total = searchResponse.HitsMetadata.Total.Value
                }
            };
            return new OkObjectResult(bookListRepresentation);
        }
    }
}